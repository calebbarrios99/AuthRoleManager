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

    public ClaimsService(
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
        var user = context.User;

        if (!user.Identity?.IsAuthenticated ?? true)
        {
            context.Fail();
            return;
        }

        var userId =
            user.FindFirst("sub")?.Value ?? user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            context.Fail();
            return;
        }

        try
        {
            // Obtener todas las claims del usuario (desde cache o BD)
            var userClaims = await GetUserClaimsAsync(userId);

            // Verificar si el usuario tiene el permiso requerido
            var hasPermission =
                userClaims.Contains(requirement.Permission) || user.IsInRole(Roles.SuperUser); // SuperUser tiene todos los permisos

            if (hasPermission)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error checking permission {Permission} for user {UserId}",
                requirement.Permission,
                userId
            );
            context.Fail();
        }
    }

    private async Task<HashSet<string>> GetUserClaimsAsync(string userId)
    {
        var cacheKey = $"user_all_claims_{userId}";

        // Intentar obtener del cache
        if (_cache.TryGetValue(cacheKey, out HashSet<string>? cachedClaims))
        {
            _logger.LogInformation("Cache user {UserId}", userId);
            return cachedClaims!;
        }

        // Si no están en cache, cargar desde la base de datos
        var userClaims = await LoadUserClaimsFromDatabaseAsync(userId);

        // Guardar en cache permanentemente
        var cacheOptions = new MemoryCacheEntryOptions
        {
            SlidingExpiration = TimeSpan.FromSeconds(5), //  Expira en 1 segundo, si no se calcula este cache en un segundo se elimina automaticamente
            // Priority = CacheItemPriority.NeverRemove, //  nunca remover automáticamente
        };

        _cache.Set(cacheKey, userClaims, cacheOptions);

        _logger.LogInformation(
            "Cache SAVED for user {UserId} - {ClaimCount} claims",
            userId,
            userClaims.Count
        );

        return userClaims;
    }

    private async Task<HashSet<string>> LoadUserClaimsFromDatabaseAsync(string userId)
    {
        var allClaims = new HashSet<string>();

        try
        {
            //  Cargar claims del rol del usuario
            var userRole = await _context
                .UserRoles.Include(x => x.Role)
                .ThenInclude(r => r != null ? r.RoleClaims : null!)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (userRole?.Role != null)
            {
                var roleName = userRole.Role.Name ?? "";
                var roleClaims = userRole.Role.RoleClaims?.ToList() ?? [];

                // Agregar el rol como claim especial
                if (!string.IsNullOrEmpty(roleName))
                {
                    allClaims.Add($"role:{roleName}");
                }

                // Agregar todas las claims del rol
                foreach (
                    var roleClaim in roleClaims.Where(rc =>
                        rc.ClaimType == "permission" && !string.IsNullOrEmpty(rc.ClaimValue)
                    )
                )
                {
                    allClaims.Add(roleClaim.ClaimValue!);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading claims from database for user {UserId}", userId);
            throw;
        }

        return allClaims;
    }

    public void InvalidateUserClaims(string userId)
    {
        var cacheKey = $"user_all_claims_{userId}";
        _cache.Remove(cacheKey);
        _logger.LogInformation("Cache INVALIDATED for user {UserId}", userId);
    }
}
