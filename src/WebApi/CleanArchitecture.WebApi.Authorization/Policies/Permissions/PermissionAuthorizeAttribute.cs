using Microsoft.AspNetCore.Authorization;

namespace CleanArchitecture.WebApi.Authorization.Policies.Permissions;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public sealed class PermissionAuthorizeAttribute(Permission permission)
    : AuthorizeAttribute(policy: PolicyNames.PolicyName(permission))
{
}