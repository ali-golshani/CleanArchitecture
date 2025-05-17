using Microsoft.AspNetCore.Authorization;

namespace CleanArchitecture.WebApi.Authorization.Policies.Scopes;

public class ScopeAuthorizeAttribute : AuthorizeAttribute
{
    public ScopeAuthorizeAttribute(Scopes scopes)
    {
        Scopes = scopes;
        Policy = PolicyNames.PolicyName(scopes);
    }

    public Scopes Scopes { get; }
}
