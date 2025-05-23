﻿using Microsoft.AspNetCore.Authorization;

namespace CleanArchitecture.WebApi.Authorization.Policies.Scopes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class ScopeAuthorizeAttribute : AuthorizeAttribute
{
    public ScopeAuthorizeAttribute(Scopes scopes)
    {
        Scopes = scopes;
        Policy = PolicyNames.PolicyName(scopes);
    }

    public Scopes Scopes { get; }
}
