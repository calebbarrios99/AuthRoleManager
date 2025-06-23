using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace AuthRoleManager.Models
{
    public class ApplicationRole : IdentityRole
    {
        public string? Description { get; set; }

        [JsonIgnore]
        public virtual ICollection<ApplicationUserRole>? UserRoles { get; set; } = [];

        [JsonIgnore]
        public virtual ICollection<IdentityRoleClaim<string>>? RoleClaims { get; set; } = [];
    }
}
