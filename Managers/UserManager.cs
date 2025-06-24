using System.Security.Claims;
using AuthRoleManager.Data;
using AuthRoleManager.Models;
using AuthRoleManager.Models.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;

namespace AuthRoleManager.Managers;

public class UserManager
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ILogger<UserManager> _logger;
    private readonly ApplicationDbContext _context;

    public UserManager(
        UserManager<ApplicationUser> userManager,
        ILogger<UserManager> logger,
        RoleManager<ApplicationRole> roleManager,
        ApplicationDbContext context
    )
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
        _context = context;
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

    public async Task<object> GetUserProfileAsync(ClaimsPrincipal userPrincipal)
    {
        if (!userPrincipal.Identity?.IsAuthenticated ?? true)
        {
            throw new UnauthorizedAccessException("User is not authenticated");
        }

        var userId =
            userPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? userPrincipal.FindFirst("sub")?.Value;

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
            throw new ArgumentException("User not found in database");
        }

        return new
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
            expiresAtFormatted = userPrincipal.FindFirst("oi_exp_dt")?.Value,

            // Información de autenticación
            issuedAt = userPrincipal.FindFirst("iat")?.Value,
            expiresAt = userPrincipal.FindFirst("exp")?.Value,
        };
    }

    public string GetUserEmail(ClaimsPrincipal userPrincipal)
    {
        if (!userPrincipal.Identity?.IsAuthenticated ?? true)
        {
            throw new UnauthorizedAccessException();
        }

        var email = userPrincipal.FindFirst(ClaimTypes.Email)?.Value;

        if (string.IsNullOrEmpty(email))
        {
            throw new ArgumentException("Email not found in token");
        }

        return email;
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
