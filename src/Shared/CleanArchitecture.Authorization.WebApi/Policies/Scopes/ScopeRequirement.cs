using Microsoft.AspNetCore.Authorization;

namespace CleanArchitecture.Authorization.WebApi.Policies.Scopes;

internal class ScopeRequirement(Scopes scopes) : IAuthorizationRequirement
{
    public Scopes Scopes { get; } = scopes;
}
