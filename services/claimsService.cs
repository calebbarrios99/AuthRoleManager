using System.Security.Claims;
using AuthRoleManager.Data;
using AuthRoleManager.Models;
using AuthRoleManager.Models.Authorization;
using AuthRoleManager.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace AuthRoleManager.Services;

public class ClaimsService : AuthorizationHandler<ApiPermissionRequirement>
{
    private readonly ApplicationDbContext _context;
    private readonly IMemoryCache _cache;
    private readonly ILogger<ClaimsService> _logger;

    // Cache por 5 minutos
    private readonly TimeSpan _cacheExpiration = TimeSpan.FromMinutes(5);

    public ClaimsService(
        UserManager<ApplicationUser> userManager,
        ApplicationDbContext context,
        IMemoryCache cache,
        ILogger<ClaimsService> logger
    )
    {
        _context = context;
        _cache = cache;
        _logger = logger;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ApiPermissionRequirement requirement
    )
    {
        _logger.LogInformation(
            "🔍 Checking permission {Permission} for user {User}",
            requirement.Permission,
            context.User.Identity?.Name ?? "Unknown"
        );

        var user = context.User;

        if (!user.Identity?.IsAuthenticated ?? true)
        {
            _logger.LogWarning("❌ User not authenticated");
            context.Fail();
            return;
        }

        var userId =
            user.FindFirst("sub")?.Value ?? user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            _logger.LogWarning("❌ UserId not found in token");
            context.Fail();
            return;
        }

        _logger.LogInformation("👤 Found UserId: {UserId}", userId);

        try
        {
            // Clave única para el cache
            var cacheKey = $"user_permission_{userId}_{requirement.Permission}";

            if (_cache.TryGetValue(cacheKey, out bool cachedResult))
            {
                _logger.LogInformation(
                    "⚡ Permission check from CACHE: User {UserId}, role  Permission {Permission}, Result: {Result}",
                    userId,
                    requirement.Permission,
                    cachedResult
                );

                if (cachedResult)
                    context.Succeed(requirement);
                else
                    context.Fail();
                return;
            }

            _logger.LogInformation("🔍 Cache miss, checking database for user {UserId}", userId);

            // Si no está en cache, consultar la BD
            var hasPermission = await UserHasPermissionAsync(userId, requirement.Permission);

            // Guardar en cache
            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _cacheExpiration,
                SlidingExpiration = TimeSpan.FromMinutes(2), // Renueva si se usa
                Priority = CacheItemPriority.Normal,
            };

            _cache.Set(cacheKey, hasPermission, cacheOptions);

            _logger.LogInformation(
                "💾 Permission check from DATABASE (cached): User {UserId}, Permission {Permission}, Result: {Result}",
                userId,
                requirement.Permission,
                hasPermission
            );

            if (hasPermission)
            {
                _logger.LogInformation("✅ Access GRANTED for user {UserId}", userId);
                context.Succeed(requirement);
            }
            else
            {
                _logger.LogWarning("❌ Access DENIED for user {UserId}", userId);
                context.Fail();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "💥 Error checking permission {Permission} for user {UserId}",
                requirement.Permission,
                userId
            );
            context.Fail();
        }
    }

    private async Task<bool> UserHasPermissionAsync(string userId, string permission)
    {
        _logger.LogInformation(
            "🔍 Checking permission in database: {Permission} for user {UserId}",
            permission,
            userId
        );

        // Verificar permisos directos del usuario
        var hasDirectPermission = await _context.UserClaims.AnyAsync(uc =>
            uc.UserId == userId && uc.ClaimType == "permission" && uc.ClaimValue == permission
        );

        if (hasDirectPermission)
        {
            _logger.LogInformation(
                "✅ User {UserId} has DIRECT permission {Permission}",
                userId,
                permission
            );
            return true;
        }

        // Verificar permisos por rol
        var userRole = await _context
            .UserRoles.Include(x => x.Role)
            .ThenInclude(r => r != null ? r.RoleClaims : null!)
            .AsNoTracking() // Use AsNoTracking to avoid tracking changes to the entities
            .FirstOrDefaultAsync(u => u.UserId == userId);

        var role = userRole?.Role?.Name ?? "";
        var claims = userRole?.Role?.RoleClaims?.ToList() ?? new List<IdentityRoleClaim<string>>();

        _logger.LogInformation(
            "👤 User {UserId} has role: {Role} with {ClaimCount} claims",
            userId,
            role,
            claims.Count
        );

        // Verificar si el rol tiene el permiso solicitado
        var hasRolePermission =
            claims.Any(c => c.ClaimType == "permission" && c.ClaimValue == permission)
            || role == Roles.SuperUser;

        if (hasRolePermission)
        {
            _logger.LogInformation(
                "✅ User {UserId} has ROLE permission {Permission} via role {Role}",
                userId,
                permission,
                role
            );
        }
        else
        {
            _logger.LogWarning(
                "❌ User {UserId} does NOT have permission {Permission}",
                userId,
                permission
            );
        }

        return hasRolePermission;
    }
}
