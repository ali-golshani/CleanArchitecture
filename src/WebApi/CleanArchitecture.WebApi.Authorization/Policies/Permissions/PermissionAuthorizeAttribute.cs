using Microsoft.AspNetCore.Authorization;

namespace CleanArchitecture.WebApi.Authorization.Policies.Permissions;

public sealed class PermissionAuthorizeAttribute(Permission permission)
    : AuthorizeAttribute(policy: PolicyNames.PolicyName(permission))
{
}