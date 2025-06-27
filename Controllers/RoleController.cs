using AuthRoleManager.Data;
using AuthRoleManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Validation.AspNetCore;

namespace AuthRoleManager.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(
    Roles = "SuperUser",
    AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme
)]
public class RoleController : ControllerBase
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ApplicationDbContext _context;
    private readonly ILogger<RoleController> _logger;

    public RoleController(
        RoleManager<ApplicationRole> roleManager,
        ApplicationDbContext context,
        ILogger<RoleController> logger
    )
    {
        _roleManager = roleManager;
        _context = context;
        _logger = logger;
    }

    // GET: api/role
    [HttpGet]
    public async Task<IActionResult> GetAllRoles()
    {
        var roles = await _roleManager
            .Roles.Select(r => new
            {
                Id = r.Id,
                Name = r.Name,
                Claims = r.RoleClaims,
            })
            .ToListAsync();

        return Ok(roles);
    }

    // GET: api/role/{roleId}/permissions
    [HttpGet("{roleId}/permissions")]
    [Authorize(Policy = "AdminOrSuperUser")]
    public async Task<IActionResult> GetRolePermissions(string roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId);
        if (role == null)
        {
            return NotFound(new { message = "Role not found" });
        }

        var claims = await _context
            .RoleClaims.Where(rc => rc.RoleId == roleId)
            .Select(rc => new
            {
                Id = rc.Id,
                ClaimType = rc.ClaimType,
                ClaimValue = rc.ClaimValue,
            })
            .ToListAsync();

        return Ok(
            new
            {
                RoleId = role.Id,
                RoleName = role.Name,
                Permissions = claims,
            }
        );
    }

    // POST: api/role/{roleId}/permissions
    [HttpPost("{roleId}/permissions")]
    public async Task<IActionResult> AddPermissionsToRole(
        string roleId,
        [FromBody] AddPermissionsRequest request
    )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var role = await _roleManager.FindByIdAsync(roleId);
        if (role == null)
        {
            return NotFound(new { message = "Role not found" });
        }

        var addedPermissions = new List<object>();
        var skippedPermissions = new List<string>();

        foreach (var permission in request.Permissions)
        {
            // Verificar si el claim ya existe
            var existingClaim = await _context.RoleClaims.FirstOrDefaultAsync(rc =>
                rc.RoleId == roleId && rc.ClaimType == "permission" && rc.ClaimValue == permission
            );

            if (existingClaim != null)
            {
                skippedPermissions.Add(permission);
                continue;
            }

            // Crear nuevo claim
            var roleClaim = new IdentityRoleClaim<string>
            {
                RoleId = roleId,
                ClaimType = "permission",
                ClaimValue = permission,
            };

            _context.RoleClaims.Add(roleClaim);
            addedPermissions.Add(new { ClaimType = "permission", ClaimValue = permission });
        }

        await _context.SaveChangesAsync();

        _logger.LogInformation(
            "Added {Count} permissions to role {RoleName} by user {UserId}",
            addedPermissions.Count,
            role.Name,
            User.FindFirst("sub")?.Value
        );

        return Ok(
            new
            {
                RoleId = roleId,
                RoleName = role.Name,
                AddedPermissions = addedPermissions,
                SkippedPermissions = skippedPermissions,
                Message = $"Added {addedPermissions.Count} permissions, skipped {skippedPermissions.Count} duplicates",
            }
        );
    }

    // DELETE: api/role/{roleId}/permissions/{permission}
    [HttpDelete("{roleId}/permissions/{permission}")]
    public async Task<IActionResult> RemovePermissionFromRole(string roleId, string permission)
    {
        var role = await _roleManager.FindByIdAsync(roleId);
        if (role == null)
        {
            return NotFound(new { message = "Role not found" });
        }

        var roleClaim = await _context.RoleClaims.FirstOrDefaultAsync(rc =>
            rc.RoleId == roleId && rc.ClaimType == "permission" && rc.ClaimValue == permission
        );

        if (roleClaim == null)
        {
            return NotFound(new { message = "Permission not found for this role" });
        }

        _context.RoleClaims.Remove(roleClaim);
        await _context.SaveChangesAsync();

        _logger.LogInformation(
            "Removed permission {Permission} from role {RoleName} by user {UserId}",
            permission,
            role.Name,
            User.FindFirst("sub")?.Value
        );

        return Ok(new { message = "Permission removed successfully" });
    }
}

// DTO
public class AddPermissionsRequest
{
    public required List<string> Permissions { get; set; }
}
