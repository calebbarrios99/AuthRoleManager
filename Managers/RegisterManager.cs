using AuthRoleManager.Models;
using Microsoft.AspNetCore.Identity;

public class UserCreationManager
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ILogger<UserCreationManager> _logger;

    public UserCreationManager(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        ILogger<UserCreationManager> logger
    )
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
    }

    public async Task<object?> CreateUserWithRoleAsync(
        string email,
        string password,
        string roleName = "User"
    )
    {
        // Crear rol si no existe
        var roleExists = await _roleManager.RoleExistsAsync(roleName);
        if (!roleExists)
        {
            var role = new ApplicationRole { Name = roleName };
            var roleResult = await _roleManager.CreateAsync(role);
            if (!roleResult.Succeeded)
            {
                return new { Error = "Failed to create role" };
            }
        }

        // Crear usuario
        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true,
        };

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            _logger.LogError(
                "Failed to create user {Email}: {Errors}",
                email,
                string.Join(", ", result.Errors.Select(e => e.Description))
            );
            return new { Error = "Failed to create user" };
        }

        // Asignar rol
        await _userManager.AddToRoleAsync(user, roleName);
        _logger.LogInformation(
            "User {Email} created successfully with role {Role}.",
            user.Email,
            roleName
        );

        return new
        {
            Success = true,
            UserId = user.Id,
            Role = roleName,
        };
    }
}
