using Microsoft.AspNetCore.Authorization;

namespace CleanArchitecture.WebApi.Shared.Authorization.Scopes;

internal class ScopeRequirement(Scopes scopes) : IAuthorizationRequirement
{
    public Scopes Scopes { get; } = scopes;
}
