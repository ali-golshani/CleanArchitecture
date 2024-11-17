using CleanArchitecture.ServicesConfigurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Administration.DebugApp;

public static class ServiceCollectionBuilder
{
    private static readonly string AppPath = AppDomain.CurrentDomain.BaseDirectory;

    public static IServiceCollection Build(out IConfiguration configuration)
    {
        var configurationBuilder = new ConfigurationBuilder();
        var services = new ServiceCollection();

        configurationBuilder.SetBasePath(AppPath);

        Configuration.ConfigureAppConfiguration
        (
            configuration: configurationBuilder,
            optionsConfiguration: Configurations.GlobalSettings.OptionsConfiguration,
            secretsConfiguration: Configurations.GlobalSettings.SecretsConfiguration
        );

        configuration = configurationBuilder.Build();

        Configuration.RegisterServices(services, configuration);
        services.AddLogging(Configuration.ConfigureLogging);

        return services;
    }
}
