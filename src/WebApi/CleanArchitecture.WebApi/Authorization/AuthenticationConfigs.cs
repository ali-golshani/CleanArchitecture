namespace CleanArchitecture.WebApi.Authorization;

internal static class AuthorizationConfigs
{
    public static void Configure(IServiceCollection services)
    {
        services.AddAuthorization();
        Shared.Configs.AuthorizationConfigs.Configure(services);
        services.ConfigureOptions<ConfigureAuthorizationOptions>();
    }
}
