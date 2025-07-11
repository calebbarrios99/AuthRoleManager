using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using AuthRoleManager.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthRoleManager.Models;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    [StringLength(255)]
    public string? LastName { get; set; }

    [StringLength(255)]
    public string? FirstName { get; set; }

    [StringLength(255)]
    public string? Address { get; set; }

    [JsonIgnore]
    public virtual ICollection<IdentityUserClaim<string>>? Claims { get; set; } = [];

    [JsonIgnore]
    public virtual ICollection<IdentityUserLogin<string>>? Logins { get; set; } = [];

    [JsonIgnore]
    public virtual ICollection<IdentityUserToken<string>>? Tokens { get; set; } = [];

    [JsonIgnore]
    public virtual ICollection<ApplicationUserRole>? UserRoles { get; set; } = [];
}

public class ApplicationUserRole : IdentityUserRole<string>
{
    public virtual ApplicationUser? User { get; set; }
    public virtual ApplicationRole? Role { get; set; }
}
