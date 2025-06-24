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
        /// <summary>
        [HttpGet()]
        public async Task<IActionResult> GetProfileAsync()
        {
            try
            {
                var profile = await _userManager.GetUserProfileAsync(User);
                return Ok(profile);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { error = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { error = ex.Message });
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
    }
}
