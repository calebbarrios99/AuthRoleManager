using System.Security.Claims;
using AuthRoleManager.Data;
using AuthRoleManager.Models;
using AuthRoleManager.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OpenIddict.Validation.AspNetCore;
using Quartz;
using static OpenIddict.Abstractions.OpenIddictConstants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(connectionString);
    options.UseOpenIddict();
});

builder.Services.AddControllers();

builder.Services.AddControllers();

#region Managers
builder.Services.AddScoped<LoginManager>();
builder.Services.AddScoped<UserCreationManager>();

#endregion

#region  identity
// Register the Identity services.
builder
    .Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
#endregion

// Configure Identity options
builder.Services.Configure<IdentityOptions>(options =>
{
    options.ClaimsIdentity.UserNameClaimType = Claims.Name;
    options.ClaimsIdentity.UserIdClaimType = Claims.Subject;
    options.ClaimsIdentity.RoleClaimType = Claims.Role;

    // Password settings
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
});

// OpenIddict offers native integration with Quartz.NET to perform scheduled tasks
// (like pruning orphaned authorizations/tokens from the database) at regular intervals.
builder.Services.AddQuartz(options =>
{
    options.UseSimpleTypeLoader();
    options.UseInMemoryStore();
});

// Register the Quartz.NET service and configure it to block shutdown until jobs are complete.
builder.Services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

// Configure OpenIddict
builder
    .Services.AddOpenIddict()
    // Register the OpenIddict core components.
    .AddCore(options =>
    {
        // Configure OpenIddict to use the Entity Framework Core stores and models.
        // Note: call ReplaceDefaultEntities() to replace the default OpenIddict entities.
        options.UseEntityFrameworkCore().UseDbContext<ApplicationDbContext>();

        // Enable Quartz.NET integration.
        options.UseQuartz();
    })
    // Register the OpenIddict server components.
    .AddServer(options =>
    {
        // Enable the token endpoint.
        options.SetTokenEndpointUris("connect/token");

        options.RegisterScopes(Scopes.Email, Scopes.Profile, Scopes.OpenId, Scopes.Roles);

        // Enable the password flow and refresh token flow.
        options.AllowPasswordFlow().AllowRefreshTokenFlow();

        // Accept anonymous clients (no client authentication).
        options.AcceptAnonymousClients();

        // Register the signing and encryption credentials.
        options.AddDevelopmentEncryptionCertificate().AddDevelopmentSigningCertificate();

        // Set token lifetimes
        options.SetAccessTokenLifetime(TimeSpan.FromDays(30));
        options.SetRefreshTokenLifetime(TimeSpan.FromDays(365));

        // Disable access token encryption for development
        options.DisableAccessTokenEncryption();

        // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
        options.UseAspNetCore().EnableTokenEndpointPassthrough();
    })
    // Register the OpenIddict validation components.
    .AddValidation(options =>
    {
        // Import the configuration from the local OpenIddict server instance.
        options.UseLocalServer();

        // Register the ASP.NET Core host.
        options.UseAspNetCore();
    });

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Version = "v1.0.0",
            Title = "AuthRoleManager API",
            Description =
                "API con sistema de autenticación OpenIddict y autorización basada en claims",
            Contact = new OpenApiContact { Name = "Developer", Email = "developer@example.com" },
        }
    );

    options.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Description =
                "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
        }
    );

    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer",
                    },
                },
                Array.Empty<string>()
            },
        }
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthRoleManager API v1");
        options.RoutePrefix = "swagger";

        // Habilitar funciones de copia y prueba
        options.EnableTryItOutByDefault();
        options.DisplayRequestDuration();
        options.EnableDeepLinking();
        options.EnableFilter();
        options.ShowExtensions();
        options.EnableValidator();

        // Configuración para mostrar URLs completas
        options.ConfigObject.AdditionalItems["syntaxHighlight"] = new Dictionary<string, object>
        {
            ["activated"] = true,
            ["theme"] = "monokai",
        };
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Ruta raíz
app.MapGet("/", () => "Hello world");

app.MapControllers();

// Seed data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

    await context.Database.EnsureCreatedAsync(); // Create roles if they don't exist
    if (!await roleManager.RoleExistsAsync(Roles.Admin))
    {
        var adminRole = new ApplicationRole
        {
            Name = Roles.Admin,
            Description = "Administrator role",
        };
        await roleManager.CreateAsync(adminRole);

        // Add permissions to Admin role
        var adminPermissions = new[]
        {
            "roles.view",
            "roles.edit",
            "roles.assign",
            "users.view",
            "users.create",
            "users.edit",
            "users.delete",
        };

        foreach (var permission in adminPermissions)
        {
            await roleManager.AddClaimAsync(
                adminRole,
                new Claim(CustomClaimTypes.Permission, permission)
            );
        }
    }

    if (!await roleManager.RoleExistsAsync(Roles.User))
    {
        var userRole = new ApplicationRole { Name = Roles.User, Description = "Regular user role" };
        await roleManager.CreateAsync(userRole);
    }

    // Create default admin user
    var adminUser = await userManager.FindByNameAsync("admin");
    if (adminUser == null)
    {
        adminUser = new ApplicationUser
        {
            UserName = "admin",
            Email = "admin@example.com",
            LastName = "Administrator",
            EmailConfirmed = true,
        };
        await userManager.CreateAsync(adminUser, "Admin123!");
        await userManager.AddToRoleAsync(adminUser, Roles.Admin);
    }

    // Create default regular user
    var regularUser = await userManager.FindByNameAsync("user");
    if (regularUser == null)
    {
        regularUser = new ApplicationUser
        {
            UserName = "user",
            Email = "user@example.com",
            LastName = "User",
            EmailConfirmed = true,
        };
        await userManager.CreateAsync(regularUser, "User123!");
        await userManager.AddToRoleAsync(regularUser, Roles.User);
    }
}

app.Run();
