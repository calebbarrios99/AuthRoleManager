using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AuthRoleManager.Utilities
{
    public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _claimType;
        private readonly string _claimValue;

        public string? Roles { get; set; }

        public CustomAuthorizeAttribute(string claimType, string claimValue)
        {
            _claimType = claimType;
            _claimValue = claimValue;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity?.IsAuthenticated ?? true)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Verificar roles si se especifican
            if (!string.IsNullOrEmpty(Roles))
            {
                var roles = Roles.Split(',').Select(r => r.Trim());
                if (!roles.Any(role => user.IsInRole(role)))
                {
                    context.Result = new ForbidResult();
                    return;
                }
            }

            // Verificar claims
            if (!user.HasClaim(_claimType, _claimValue))
            {
                context.Result = new ForbidResult();
                return;
            }
        }
    }
}
