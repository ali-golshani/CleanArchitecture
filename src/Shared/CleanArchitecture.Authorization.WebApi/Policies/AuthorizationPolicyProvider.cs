using CleanArchitecture.Authorization.WebApi.Policies.Permissions;
using CleanArchitecture.Authorization.WebApi.Policies.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.Authorization.WebApi.Policies;

public sealed class AuthorizationPolicyProvider(
    IOptions<AuthorizationOptions> options,
    RoleAuthorizationPolicyProvider roleAuthorizationPolicy,
    PermissionAuthorizationPolicyProvider permissionAuthorizationPolicy)
    : DefaultAuthorizationPolicyProvider(options)
{
    private readonly RoleAuthorizationPolicyProvider roleAuthorizationPolicy = roleAuthorizationPolicy;
    private readonly PermissionAuthorizationPolicyProvider permissionAuthorizationPolicy = permissionAuthorizationPolicy;

    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        return
            await base.GetPolicyAsync(policyName) ??
            roleAuthorizationPolicy.GetPolicy(policyName) ??
            permissionAuthorizationPolicy.GetPolicy(policyName);
    }
}