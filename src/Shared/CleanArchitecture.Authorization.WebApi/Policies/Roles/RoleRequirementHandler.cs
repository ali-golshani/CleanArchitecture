using Microsoft.AspNetCore.Authorization;

namespace CleanArchitecture.Authorization.WebApi.Policies.Roles;

internal class RoleRequirementHandler : AuthorizationHandler<RoleRequirement>
{
    private readonly Roles[] allRoles;

    public RoleRequirementHandler()
    {
        allRoles = [.. Enum.GetValues<Roles>().Except([Roles.None])];
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        RoleRequirement requirement)
    {
        var roles = requirement.Roles;

        foreach (var role in allRoles)
        {
            if ((role & roles) == role)
            {
                var roleName = RoleNames.RoleName(role);
                if (context.User.HasClaim(type: RoleNames.ClaimType, value: roleName))
                {
                    context.Succeed(requirement);
                    break;
                }
            }
        }

        return Task.CompletedTask;
    }
}
