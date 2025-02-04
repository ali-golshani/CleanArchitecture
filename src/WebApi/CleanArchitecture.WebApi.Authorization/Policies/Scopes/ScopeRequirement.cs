using Microsoft.AspNetCore.Authorization;

namespace CleanArchitecture.WebApi.Authorization.Policies.Scopes;

internal class ScopeRequirement(Scopes scopes) : IAuthorizationRequirement
{
    public Scopes Scopes { get; } = scopes;
}
