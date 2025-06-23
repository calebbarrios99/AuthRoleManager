using AuthRoleManager.Data;
using AuthRoleManager.Models;
using Microsoft.AspNetCore.Identity;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace AuthRoleManager.Services;

public class DbFeed(IServiceProvider services, ILogger<DbFeed> logger) : IHostedService
{
    private readonly IServiceProvider _services = services;
    private readonly ILogger _logger = logger;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("DbFeed service is starting.");
        using var serviceScope = _services.CreateScope();
        var services = serviceScope.ServiceProvider;

        var dbContext = services.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.EnsureCreated();
        _logger.LogInformation("Database created or already exists.");

        // await SeedCredentials(dbContext);

        await CreateUsers();
    }

    private async Task CreateUsers()
    {
        using var serviceScope = _services.CreateScope();
        var userManager = serviceScope.ServiceProvider.GetRequiredService<
            UserManager<ApplicationUser>
        >();
        var roleManager = serviceScope.ServiceProvider.GetRequiredService<
            RoleManager<ApplicationRole>
        >();

        // Verificar si el rol ya existe antes de crearlo
        var roleName = "SuperUser";
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            var role = new ApplicationRole { Name = roleName };
            var roleResult = await roleManager.CreateAsync(role);

            if (roleResult.Succeeded)
            {
                _logger.LogInformation("Role {RoleName} created successfully.", roleName);
            }
            else
            {
                _logger.LogError(
                    "Failed to create role {RoleName}: {Errors}",
                    roleName,
                    string.Join(", ", roleResult.Errors.Select(e => e.Description))
                );
                return;
            }
        }

        // Verificar si el usuario ya existe antes de crearlo
        var userEmail = "superuser@mail.com";
        var existingUser = await userManager.FindByEmailAsync(userEmail);

        if (existingUser == null)
        {
            var user = new ApplicationUser
            {
                UserName = userEmail,
                Email = userEmail,
                LastName = "SuperUser",
                Address = "123 Main St",
                EmailConfirmed = true,
            };

            var result = await userManager.CreateAsync(user, "Abc123*");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, roleName);
                _logger.LogInformation("User {Email} created successfully.", user.Email);
            }
            else
            {
                _logger.LogError(
                    "Failed to create user {Email}: {Errors}",
                    user.Email,
                    string.Join(", ", result.Errors.Select(e => e.Description))
                );
            }
        }
        else
        {
            _logger.LogInformation("User {Email} already exists.", userEmail);
        }
    }

    public async Task SeedCredentials(ApplicationDbContext dbContext)
    {
        using var serviceScope = _services.CreateScope();
        var manager =
            serviceScope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

        if (await manager.FindByClientIdAsync("nutresa") == null)
        {
            await manager.CreateAsync(
                new OpenIddictApplicationDescriptor
                {
                    ClientId = "nutresa",
                    ClientSecret = "388D45FA-B36B-4988-BA59-B187D329C207",
                    DisplayName = "nutresa client application",
                    Permissions =
                    {
                        Permissions.Endpoints.Token,
                        Permissions.GrantTypes.ClientCredentials,
                    },
                }
            );
        }
        if (await manager.FindByClientIdAsync("alpina") == null)
        {
            await manager.CreateAsync(
                new OpenIddictApplicationDescriptor
                {
                    ClientId = "alpina",
                    ClientSecret = "27b7eccf-72fa-4575-9b1e-51cd176d2437",
                    DisplayName = "alpina client application",
                    Permissions =
                    {
                        Permissions.Endpoints.Token,
                        Permissions.GrantTypes.ClientCredentials,
                    },
                }
            );
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("DbFeed service is stopping.");
    }
}
