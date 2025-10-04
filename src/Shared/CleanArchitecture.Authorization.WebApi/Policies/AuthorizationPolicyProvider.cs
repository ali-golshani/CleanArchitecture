using CleanArchitecture.Authorization.WebApi.Policies.Permissions;
using CleanArchitecture.Authorization.WebApi.Policies.Scopes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.Authorization.WebApi.Policies;

public sealed class AuthorizationPolicyProvider(
    IOptions<AuthorizationOptions> options,
    ScopeAuthorizationPolicyProvider scopeAuthorizationPolicy,
    PermissionAuthorizationPolicyProvider permissionAuthorizationPolicy)
    : DefaultAuthorizationPolicyProvider(options)
{
    private readonly ScopeAuthorizationPolicyProvider scopeAuthorizationPolicy = scopeAuthorizationPolicy;
    private readonly PermissionAuthorizationPolicyProvider permissionAuthorizationPolicy = permissionAuthorizationPolicy;

    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        return
            await base.GetPolicyAsync(policyName) ??
            scopeAuthorizationPolicy.GetPolicy(policyName) ??
            permissionAuthorizationPolicy.GetPolicy(policyName);
    }
}