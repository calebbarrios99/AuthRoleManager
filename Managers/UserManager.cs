using System.Security.Claims;
using AuthRoleManager.Models;
using AuthRoleManager.Models.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;

namespace AuthRoleManager.Managers;

public class UserManager
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ILogger<UserManager> _logger;

    public UserManager(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        ILogger<UserManager> logger
    )
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
    }

    // Método que recibe el ClaimsPrincipal del usuario autenticado
    public async Task<ApplicationUser> GetCurrentUserAsync(ClaimsPrincipal userPrincipal)
    {
        var user = await _userManager.GetUserAsync(userPrincipal);
        if (user == null)
        {
            throw new UnauthorizedAccessException("User not found or not authenticated");
        }
        return user;
    }

    public async Task<object?> CreateUserWithRoleAsync([FromBody] CreateUserRequest request)
    {
        {
            var roleName = "User";
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

            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Address = request.Address,
                EmailConfirmed = true,
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                _logger.LogError(
                    "Failed to create user {Email}: {Errors}",
                    request.Email,
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
}
