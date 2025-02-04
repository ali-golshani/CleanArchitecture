namespace CleanArchitecture.WebApi.Shared.Versioning;

public static class VersioningConfigs
{
    public static void Configure(IServiceCollection services)
    {
        services.AddApiVersioning().AddApiExplorer();
        services.ConfigureOptions<ConfigureApiVersioningOptions>();
        services.ConfigureOptions<ConfigureApiExplorerOptions>();
    }
}
