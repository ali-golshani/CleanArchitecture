namespace CleanArchitecture.WebApi.Shared.Authorization.Scopes;

using Microsoft.AspNetCore.Authorization;

public class ScopeAuthorizeAttribute : AuthorizeAttribute
{
    public ScopeAuthorizeAttribute(Scopes scopes)
    {
        Scopes = scopes;
        Policy = Configuration.PolicyName(scopes);
    }

    public Scopes Scopes { get; }
}
