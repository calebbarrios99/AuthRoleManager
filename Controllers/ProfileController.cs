using System.Security.Claims;
using AuthRoleManager.Data;
using AuthRoleManager.Managers;
using AuthRoleManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Validation.AspNetCore;

namespace AuthRoleManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public class ProfileController : ControllerBase
    {
        private readonly UserManager _userManager;
        private readonly ApplicationDbContext _context;

        public ProfileController(UserManager userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        /// <summary>
        /// GET /api/profile - Devuelve todos los claims del usuario logueado
        /// </summary>
        /// <returns>Lista de claims del usuario actual</returns>
        [HttpGet()]
        public async Task<IActionResult> GetProfileAsync()
        {
            try
            {
                if (!User.Identity?.IsAuthenticated ?? true)
                {
                    return Unauthorized(new { error = "User is not authenticated" });
                }

                var userId =
                    User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? User.FindFirst("sub")?.Value;

                // Consultar la tabla correcta de Identity
                var user = await _context
                    .Users // Tabla asp_net_users
                    .Where(u => u.Id == userId)
                    .Select(u => new
                    {
                        u.Id,
                        u.Email,
                        u.UserName,
                        u.FirstName,
                        u.LastName,
                    })
                    .FirstOrDefaultAsync();

                if (user == null)
                {
                    return NotFound(new { error = "User not found in database" });
                }

                return Ok(
                    new
                    {
                        userId = user.Id,
                        email = user.Email,
                        userName = user.UserName,
                        firstName = user.FirstName,
                        lastName = user.LastName,
                        fullName = $"{user.FirstName} {user.LastName}".Trim(),
                        greeting = $"Hello, {user.FirstName ?? user.UserName}!",
                        initials = GetInitials(user.FirstName, user.LastName)
                            ?? GetInitialsFromName($"{user.FirstName} {user.LastName}"),

                        // Claims de OpenIddict específicos
                        expiresAtFormatted = User.FindFirst("oi_exp_dt")?.Value,

                        // Información de autenticación
                        issuedAt = User.FindFirst("iat")?.Value,
                        expiresAt = User.FindFirst("exp")?.Value,
                    }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        /// <summary>
        /// GET /api/profile/email - Devuelve el email si está en el token
        /// </summary>
        /// <returns>Email del usuario si existe en el token</returns>
        [HttpGet("email")]
        public IActionResult GetEmail()
        {
            var user = HttpContext.User;

            if (!user.Identity?.IsAuthenticated ?? true)
            {
                return Unauthorized();
            }

            var email = user.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(email))
            {
                return NotFound(new { message = "Email not found in token" });
            }

            return Ok(new { email = email });
        }

        // Método para obtener iniciales del nombre completo si no hay firstName/lastName
        private string GetInitialsFromName(string? fullName)
        {
            if (string.IsNullOrEmpty(fullName))
                return "";

            var parts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var initials = "";

            foreach (var part in parts.Take(2)) // Máximo 2 iniciales
            {
                if (!string.IsNullOrEmpty(part))
                    initials += part[0];
            }

            return initials.ToUpper();
        }

        // Actualizar GetInitials para manejar valores nulos
        private string GetInitials(string? firstName, string? lastName)
        {
            var initials = "";
            if (!string.IsNullOrEmpty(firstName))
                initials += firstName[0];
            if (!string.IsNullOrEmpty(lastName))
                initials += lastName[0];
            return initials.ToUpper();
        }
    }
}
