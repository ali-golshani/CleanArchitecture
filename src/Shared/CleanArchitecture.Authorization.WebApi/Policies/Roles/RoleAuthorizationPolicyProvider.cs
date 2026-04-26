using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.Authorization.WebApi.Policies.Roles;

public sealed class RoleAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
{
    private readonly AuthorizationOptions options = options.Value;

    public AuthorizationPolicy? GetPolicy(string policyName)
    {
        if (!PolicyNames.TryParsePolicyName(policyName, out var roles))
        {
            return null;
        }

        var policy =
                new AuthorizationPolicyBuilder()
                .AddRequirements(new RoleRequirement(roles))
                .Build();

        options.AddPolicy(policyName, policy);

        return policy;
    }
}