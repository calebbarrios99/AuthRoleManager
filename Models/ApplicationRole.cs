using Microsoft.AspNetCore.Identity;

namespace AuthRoleManager.Models
{
    public class ApplicationRole : IdentityRole
    {
        public string? Description { get; set; }
    }
}
