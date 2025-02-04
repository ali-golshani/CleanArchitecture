using Microsoft.AspNetCore.Authorization;
using System.Reflection;

namespace CleanArchitecture.WebApi.Authorization;

public static class PolicyConfigs
{
    public static void RegisterAuthorizationPolicies(AuthorizationOptions options, Assembly assembly)
    {
        Policies.Scopes.Configuration.RegisterScopesPolicies(options, assembly);
        Policies.Permissions.Configuration.RegisterPermissionsPolicies(options);
    }
}
