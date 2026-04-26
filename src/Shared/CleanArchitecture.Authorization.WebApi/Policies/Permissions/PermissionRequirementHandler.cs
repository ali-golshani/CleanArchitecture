using Microsoft.AspNetCore.Authorization;

namespace CleanArchitecture.Authorization.WebApi.Policies.Permissions;

internal class PermissionRequirementHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var permissionName = PermissionNames.PermissionName(requirement.Permission);

        if (context.User.HasClaim(type: PermissionNames.ClaimType, value: permissionName))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
