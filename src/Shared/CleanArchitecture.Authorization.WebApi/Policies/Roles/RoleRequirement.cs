using Microsoft.AspNetCore.Authorization;

namespace CleanArchitecture.Authorization.WebApi.Policies.Roles;

internal class RoleRequirement(Roles roles) : IAuthorizationRequirement
{
    public Roles Roles { get; } = roles;
}
