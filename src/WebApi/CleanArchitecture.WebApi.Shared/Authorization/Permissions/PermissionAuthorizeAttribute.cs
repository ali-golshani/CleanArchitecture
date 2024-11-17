using Microsoft.AspNetCore.Authorization;

namespace CleanArchitecture.WebApi.Shared.Authorization.Permissions;

public sealed class PermissionAuthorizeAttribute(Permission permission)
    : AuthorizeAttribute(policy: Configuration.PolicyName(permission))
{
}