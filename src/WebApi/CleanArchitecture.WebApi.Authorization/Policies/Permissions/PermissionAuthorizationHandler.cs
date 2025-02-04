using Microsoft.AspNetCore.Authorization;

namespace CleanArchitecture.WebApi.Authorization.Policies.Permissions;

internal class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var permissionName = Configuration.PermissionName(requirement.Permission);

        if (context.User.HasClaim(type: Configuration.ClaimType, value: permissionName))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
