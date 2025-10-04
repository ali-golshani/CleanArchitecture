using CleanArchitecture.Authorization.WebApi;

namespace CleanArchitecture.WebApi.OData.Authorization;

internal static class AuthorizationConfigs
{
    public static void Configure(IServiceCollection services)
    {
        services.AddAuthorization();
        PolicyConfigs.RegisterServices(services);
        services.ConfigureOptions<ConfigureAuthorizationOptions>();
    }
}
