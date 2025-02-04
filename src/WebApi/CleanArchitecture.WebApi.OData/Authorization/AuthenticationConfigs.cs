namespace CleanArchitecture.WebApi.OData.Authorization;

internal static class AuthorizationConfigs
{
    public static void Configure(IServiceCollection services)
    {
        services.AddAuthorization();
        WebApi.Authorization.ServiceConfigurations.RegisterServices(services);
        services.ConfigureOptions<ConfigureAuthorizationOptions>();
    }
}
