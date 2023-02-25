using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Numerology.API.Claims;
using Numerology.API.Managers;

namespace Numerology.API.Authorization.Requirments
{
    public class ViewRoleAuthorizationRequirement : IAuthorizationRequirement
    {

    }

    public class ViewRoleAuthorizationHandler : AuthorizationHandler<ViewRoleAuthorizationRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ViewRoleAuthorizationRequirement requirement, string roleName)
        {
            if (context.User == null)
                return Task.CompletedTask;

            if (context.User.HasClaim(CustomClaimTypes.Permission, ApplicationPermissions.ViewRoles) || context.User.IsInRole(roleName))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
