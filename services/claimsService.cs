using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using AuthRoleManager.Models;
using AuthRoleManager.Data;
using AuthRoleManager.Services.Authorization;
using Microsoft.AspNetCore.Authorization;

public class ClaimsService : AuthorizationHandler<DatabasePermissionRequirement>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;
    private readonly IMemoryCache _cache;
    private readonly ILogger<ClaimsService> _logger;

    // Cache por 5 minutos
    private readonly TimeSpan _cacheExpiration = TimeSpan.FromMinutes(5);

    public ClaimsService(
        UserManager<ApplicationUser> userManager,
        ApplicationDbContext context,
        IMemoryCache cache,
        ILogger<ClaimsService> logger)
    {
        _userManager = userManager;
        _context = context;
        _cache = cache;
        _logger = logger;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        DatabasePermissionRequirement requirement)
    {
        var user = context.User;

        if (!user.Identity?.IsAuthenticated ?? true)
        {
            context.Fail();
            return;
        }

        var userId = user.FindFirst("sub")?.Value ?? user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            context.Fail();
            return;
        }

        try
        {
            // Clave única para el cache
            var cacheKey = $"user_permission_{userId}_{requirement.Permission}";

            if (_cache.TryGetValue(cacheKey, out bool cachedResult))
            {
                _logger.LogDebug("Permission check from CACHE: User {UserId}, Permission {Permission}, Result: {Result}",
                    userId, requirement.Permission, cachedResult);

                if (cachedResult)
                    context.Succeed(requirement);
                else
                    context.Fail();
                return;
            }

            // Si no está en cache, consultar la BD
            var hasPermission = await UserHasPermissionAsync(userId, requirement.Permission);

            // Guardar en cache
            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = _cacheExpiration,
                SlidingExpiration = TimeSpan.FromMinutes(2), // Renueva si se usa
                Priority = CacheItemPriority.Normal
            };

            _cache.Set(cacheKey, hasPermission, cacheOptions);

            _logger.LogDebug("Permission check from DATABASE (cached): User {UserId}, Permission {Permission}, Result: {Result}",
                userId, requirement.Permission, hasPermission);

            if (hasPermission)
                context.Succeed(requirement);
            else
                context.Fail();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking permission {Permission} for user {UserId}", requirement.Permission, userId);
            context.Fail();
        }
    }

    private async Task<bool> UserHasPermissionAsync(string userId, string permission)
    {
        // Verificar permisos directos del usuario
        var hasDirectPermission = await _context.UserClaims
            .AnyAsync(uc => uc.UserId == userId &&
                           uc.ClaimType == "permission" &&
                           uc.ClaimValue == permission);

        if (hasDirectPermission)
            return true;

        // Verificar permisos por rol
        var userRole = await _context.UserRoles
            .Include(x => x.Role)
            .ThenInclude(r => r.RoleClaims)
            .AsNoTracking() // Use AsNoTracking to avoid tracking changes to the entities
            .FirstOrDefaultAsync(u => u.UserId == userId);

        var role = userRole?.Role?.Name ?? "";
        var claims = userRole?.Role?.RoleClaims?.ToList() ?? new List<IdentityRoleClaim<string>>();

        // Verificar si el rol tiene el permiso solicitado
        var hasRolePermission = claims.Any(c => c.ClaimType == "permission" && c.ClaimValue == permission);

        _logger.LogDebug("Permission check for user {UserId}: Role={Role}, Permission={Permission}, HasPermission={HasPermission}",
            userId, role, permission, hasRolePermission);

        return hasRolePermission;
    }
}




namespace AuthRoleManager.Services.Authorization;

public class DatabasePermissionRequirement : IAuthorizationRequirement
{
    public string Permission { get; }

    public DatabasePermissionRequirement(string permission)
    {
        Permission = permission;
    }
}
