using Microsoft.AspNetCore.Authorization;
using System.Reflection;

namespace CleanArchitecture.WebApi.Shared.Authorization.Scopes;

internal static class Configuration
{
    public const string ClaimType = "scope";

    public static string PolicyName(Scopes scopes)
    {
        return $"ScopePolicy-{(int)scopes}";
    }

    public static string ScopeName(Scopes scopes)
    {
        return ScopeNames.ScopeName(scopes);
    }

    public static void RegisterScopesPolicies(AuthorizationOptions options, Assembly assembly)
    {
        var scopeAttributes =
            assembly
            .GetTypes()
            .SelectMany(type => type.GetMethods())
            .SelectMany(method => method.GetCustomAttributes(typeof(ScopeAuthorizeAttribute), inherit: true))
            .Cast<ScopeAuthorizeAttribute>();

        foreach (var attribute in scopeAttributes)
        {
            AddPolicies(options, attribute.Scopes);
        }
    }

    private static void AddPolicies(AuthorizationOptions options, Scopes scopes)
    {
        var policyName = PolicyName(scopes);
        options.AddPolicy(policyName, policy =>
        {
            policy.Requirements.Add(new ScopeRequirement(scopes));
        });
    }
}
