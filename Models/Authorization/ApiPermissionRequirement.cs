namespace AuthRoleManager.Models.Authorization;

using Microsoft.AspNetCore.Authorization;

public class ApiPermissionRequirement : IAuthorizationRequirement
{
    public string Permission { get; }

    public ApiPermissionRequirement(string permission)
    {
        Permission = permission;
    }
}
