using System.Collections;
using AuthRoleManager.Models.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Validation.AspNetCore;

[ApiController]
[Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
[Route("api/[controller]")]
public class RegisterController : ControllerBase
{
    private readonly UserCreationManager _userCreationManager;

    public RegisterController(UserCreationManager userCreationManager)
    {
        _userCreationManager = userCreationManager;
    }

    [HttpPost("create/admin")]
    public async Task<IActionResult> CreateAdmin([FromBody] CreateUserRequest request)
    {
        var result = await _userCreationManager.CreateUserWithRoleAsync(request);
        return Ok(result);
    }

    // [HttpPost("create/user")]
    // public async Task<IActionResult> CreateUser([FromBody] dynamic request)
    // {
    //     string email = request.email;
    //     string password = request.password;

    //     var result = await _userCreationManager.CreateUserWithRoleAsync(email, password, "User");
    //     return Ok(result);
    // }
}
