using Microsoft.AspNetCore.Authorization;

namespace CleanArchitecture.Authorization.WebApi.Policies.Roles;

internal class RoleRequirement(Claims.Roles roles) : IAuthorizationRequirement
{
    public Claims.Roles Roles { get; } = roles;
}
