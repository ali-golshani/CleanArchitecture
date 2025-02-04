using CleanArchitecture.Actors;
using CleanArchitecture.WebApi.Authorization.Actors;
using CleanArchitecture.WebApi.Authorization.Policies.Permissions;
using CleanArchitecture.WebApi.Authorization.Policies.Scopes;
using Microsoft.AspNetCore.Authorization;

namespace CleanArchitecture.WebApi.Authorization;

public static class ServiceConfigurations
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IActorProvider, HttpActorProvider>();

        services.AddSingleton<IAuthorizationHandler, ScopeAuthorizationHandler>();
        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
    }
}
