using AuthRoleManager.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AuthRoleManager.Controllers;

/// <summary>
/// Initializes a new instance of the <see cref="TodoController"/> class.
/// This constructor takes a <see cref="ApplicationDbContext"/> context to manage todos.
/// </summary>
/// <param name="dbContext">The service context for managing todos.</param>
[Route("api/[controller]")]
[ApiController]
public class AuthController(LoginManager loginManager) : ControllerBase
{
    private readonly LoginManager _loginManager = loginManager;

    // POST: api/auth/login
    [HttpPost("[action]")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel model)
    {
        var res = await _loginManager.ValidateUserAsync(model);
        if (res == null)
        {
            ModelState.AddModelError("Login", "Invalid email or password.");
            return BadRequest(ModelState);
        }
        return Ok(res);
    }
}
