namespace CleanArchitecture.WebApi.Authorization;

internal static class AuthorizationConfigs
{
    public static void Configure(IServiceCollection services)
    {
        services.AddAuthorization();
        ServiceConfigurations.RegisterServices(services);
        services.ConfigureOptions<ConfigureAuthorizationOptions>();
    }
}
