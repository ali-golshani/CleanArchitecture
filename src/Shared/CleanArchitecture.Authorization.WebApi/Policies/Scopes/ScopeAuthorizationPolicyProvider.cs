using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.Authorization.WebApi.Policies.Scopes;

public sealed class ScopeAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
{
    private readonly AuthorizationOptions options = options.Value;

    public AuthorizationPolicy? GetPolicy(string policyName)
    {
        if (!PolicyNames.TryParsePolicyName(policyName, out var scopes))
        {
            return null;
        }

        var policy =
                new AuthorizationPolicyBuilder()
                .AddRequirements(new ScopeRequirement(scopes))
                .Build();

        options.AddPolicy(policyName, policy);

        return policy;
    }
}