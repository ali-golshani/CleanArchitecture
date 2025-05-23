﻿using CleanArchitecture.WebApi.Authorization.Policies.Permissions;
using CleanArchitecture.WebApi.Authorization.Policies.Scopes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.WebApi.Authorization;

public static class PolicyConfigs
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationHandler, ScopeRequirementHandler>();
        services.AddSingleton<IAuthorizationHandler, PermissionRequirementHandler>();

        services.AddSingleton<IAuthorizationPolicyProvider, ScopeAuthorizationPolicyProvider>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
    }
}
