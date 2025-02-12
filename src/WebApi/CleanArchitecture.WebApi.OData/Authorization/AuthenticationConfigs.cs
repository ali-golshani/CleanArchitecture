namespace CleanArchitecture.WebApi.OData.Authorization;

internal static class AuthorizationConfigs
{
    public static void Configure(IServiceCollection services)
    {
        services.AddAuthorization();
        WebApi.Authorization.PolicyConfigs.RegisterServices(services);
        services.ConfigureOptions<ConfigureAuthorizationOptions>();
    }
}
