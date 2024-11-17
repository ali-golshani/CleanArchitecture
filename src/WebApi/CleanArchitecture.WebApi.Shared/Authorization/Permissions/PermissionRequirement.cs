using Microsoft.AspNetCore.Authorization;

namespace CleanArchitecture.WebApi.Shared.Authorization.Permissions;

internal class PermissionRequirement(Permission permission) : IAuthorizationRequirement
{
    public Permission Permission { get; } = permission;
}
