using CleanArchitecture.WebApi.Authorization.Policies.Permissions;
using CleanArchitecture.WebApi.Authorization.Policies.Scopes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanArchitecture.WebApi.Authorization;

public static class PolicyConfigs
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationHandler, ScopeAuthorizationHandler>();
        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
    }

    public static void RegisterAuthorizationPolicies(AuthorizationOptions options, Assembly assembly)
    {
        Policies.Scopes.Configuration.RegisterScopesPolicies(options, assembly);
        Policies.Permissions.Configuration.RegisterPermissionsPolicies(options);
    }
}
