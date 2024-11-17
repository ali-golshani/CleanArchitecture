namespace CleanArchitecture.WebApi.Shared.Cors;

public static class CorsConfigs
{
    public static void Configure(this IServiceCollection services)
    {
        services.AddCors();
        services.ConfigureOptions<ConfigureCorsOptions>();
    }
}
