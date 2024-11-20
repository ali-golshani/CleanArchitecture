using CleanArchitecture.Configurations;
using CleanArchitecture.ServicesConfigurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.IntegrationTests;

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
            environment: SystemEnvironment.Environment
        );

        configuration = configurationBuilder.Build();

        Configuration.ConfigureServices(services, configuration);
        services.AddLogging(Configuration.ConfigureLogging);

        return services;
    }
}
