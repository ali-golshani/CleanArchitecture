using CleanArchitecture.Authorization.WebApi.Policies.Permissions;
using CleanArchitecture.Authorization.WebApi.Policies.Scopes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Authorization.WebApi;

public static class PolicyConfigs
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationHandler, ScopeRequirementHandler>();
        services.AddSingleton<IAuthorizationHandler, PermissionRequirementHandler>();

        services.AddSingleton<ScopeAuthorizationPolicyProvider>();
        services.AddSingleton<PermissionAuthorizationPolicyProvider>();
        services.AddSingleton<IAuthorizationPolicyProvider, Policies.AuthorizationPolicyProvider>();
    }
}
