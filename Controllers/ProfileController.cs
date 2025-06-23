using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;

namespace AuthRoleManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    public class ProfileController : ControllerBase
    {
        /// <summary>
        /// GET /api/profile - Devuelve todos los claims del usuario logueado
        /// </summary>
        /// <returns>Lista de claims del usuario actual</returns>
        [HttpGet]
        public IActionResult GetProfile()
        {
            var user = HttpContext.User;

            if (!user.Identity?.IsAuthenticated ?? true)
            {
                return Unauthorized();
            }

            var claims = user.Claims.Select(c => new { type = c.Type, value = c.Value }).ToList();

            var userInfo = new
            {
                id = user.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                username = user.FindFirst(ClaimTypes.Name)?.Value,
                email = user.FindFirst(ClaimTypes.Email)?.Value,
                roles = user.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList(),
                claims = claims,
            };

            return Ok(userInfo);
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
    }
}
