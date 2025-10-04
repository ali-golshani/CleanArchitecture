using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.Authorization.WebApi.Policies.Permissions;

public sealed class PermissionAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
{
    private readonly AuthorizationOptions options = options.Value;

    public AuthorizationPolicy? GetPolicy(string policyName)
    {
        if (!PolicyNames.TryParsePolicyName(policyName, out var permission))
        {
            return null;
        }

        var policy =
                new AuthorizationPolicyBuilder()
                .AddRequirements(new PermissionRequirement(permission))
                .Build();

        options.AddPolicy(policyName, policy);

        return policy;
    }
}