using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.WebApi.Authorization.Policies.Scopes;

public class ScopeAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : DefaultAuthorizationPolicyProvider(options)
{
    private readonly AuthorizationOptions options = options.Value;

    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        var policy = await base.GetPolicyAsync(policyName);

        if (policy is null && PolicyNames.TryParsePolicyName(policyName, out var scopes))
        {
            policy =
                new AuthorizationPolicyBuilder()
                .AddRequirements(new ScopeRequirement(scopes))
                .Build();

            options.AddPolicy(policyName, policy);
        }

        return policy;
    }
}