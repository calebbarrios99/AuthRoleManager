using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthRoleManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        // Simulación de almacenamiento de roles y permisos
        private static readonly Dictionary<string, List<string>> RolePermissions = new();

        // Solo el rol "superUser" puede acceder a este endpoint
        [HttpPost("{roleName}/permissions")]
        [Authorize(Roles = "superUser")]
        public async Task<IActionResult> AddPermissionsToRole(
            string roleName,
            [FromBody] List<string> permissions
        )
        {
            if (
                string.IsNullOrWhiteSpace(roleName)
                || permissions == null
                || permissions.Count == 0
            )
                return BadRequest("Role name and permissions are required.");

            if (!RolePermissions.ContainsKey(roleName))
                RolePermissions[roleName] = new List<string>();

            foreach (var permission in permissions)
            {
                if (!RolePermissions[roleName].Contains(permission))
                    RolePermissions[roleName].Add(permission);
            }

            // Simulación de operación asíncrona
            await Task.CompletedTask;

            return Ok(new { role = roleName, permissions = RolePermissions[roleName] });
        }
    }
}
