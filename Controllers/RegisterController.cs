using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserCreationManager _userCreationManager;

    public UsersController(UserCreationManager userCreationManager)
    {
        _userCreationManager = userCreationManager;
    }

    [HttpPost("create/admin")]
    public async Task<IActionResult> CreateAdmin()
    {
        var result = await _userCreationManager.CreateUserWithRoleAsync(
            "admin@mail.com",
            "Admin123!",
            "Admin"
        );
        return Ok(result);
    }

    [HttpPost("create/user")]
    public async Task<IActionResult> CreateUser([FromBody] dynamic request)
    {
        string email = request.email;
        string password = request.password;

        var result = await _userCreationManager.CreateUserWithRoleAsync(email, password, "User");
        return Ok(result);
    }
}
