using Microsoft.AspNetCore.Authorization;

namespace CleanArchitecture.WebApi.Authorization.Policies.Scopes;

internal class ScopeAuthorizationHandler : AuthorizationHandler<ScopeRequirement>
{
    private readonly Scopes[] allScopes;

    public ScopeAuthorizationHandler()
    {
        allScopes = Enum.GetValues<Scopes>();
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        ScopeRequirement requirement)
    {
        var scopes = requirement.Scopes;

        foreach (var scope in allScopes)
        {
            if ((scope & scopes) == scope)
            {
                var scopeName = ScopeNames.ScopeName(scope);
                if (context.User.HasClaim(type: ScopeNames.ClaimType, value: scopeName))
                {
                    context.Succeed(requirement);
                    break;
                }
            }
        }

        return Task.CompletedTask;
    }
}
