using CleanArchitecture.Authorization.WebApi.Policies.Permissions;
using CleanArchitecture.Authorization.WebApi.Policies.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Authorization.WebApi;

public static class PolicyConfigs
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationHandler, RoleRequirementHandler>();
        services.AddSingleton<IAuthorizationHandler, PermissionRequirementHandler>();

        services.AddSingleton<RoleAuthorizationPolicyProvider>();
        services.AddSingleton<PermissionAuthorizationPolicyProvider>();
        services.AddSingleton<IAuthorizationPolicyProvider, Policies.AuthorizationPolicyProvider>();
    }
}
