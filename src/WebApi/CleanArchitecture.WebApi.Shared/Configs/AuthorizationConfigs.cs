using CleanArchitecture.Actors;
using CleanArchitecture.WebApi.Shared.Actors;
using CleanArchitecture.WebApi.Shared.Authorization.Permissions;
using CleanArchitecture.WebApi.Shared.Authorization.Scopes;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;

namespace CleanArchitecture.WebApi.Shared.Configs;

public static class AuthorizationConfigs
{
    public static void Configure(IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IActorProvider, HttpActorProvider>();

        services.AddSingleton<IAuthorizationHandler, ScopeAuthorizationHandler>();
        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
    }

    public static void RegisterAuthorizationPolicies(AuthorizationOptions options)
    {
        var assembly = Assembly.GetCallingAssembly();
        Authorization.Scopes.Configuration.RegisterScopesPolicies(options, assembly);
        Authorization.Permissions.Configuration.RegisterPermissionsPolicies(options);
    }
}
