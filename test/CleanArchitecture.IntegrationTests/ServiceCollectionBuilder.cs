using CleanArchitecture.Configurations;
using CleanArchitecture.IntegrationTests.Services;
using CleanArchitecture.ServicesConfigurations;
using Framework.DependencyInjection.Extensions;
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

        Configuration.SetEnvironment(ApplicationFlavor.Default, DeploymentStage.Staging);

        Configuration.ConfigureAppConfiguration
        (
            configuration: configurationBuilder,
            environment: SystemEnvironment.Environment
        );

        configuration = configurationBuilder.Build();

        Configuration.ConfigureServices(services, configuration, SystemEnvironment.Environment);
        services.AddLogging(Configuration.ConfigureLogging);

        services.RegisterAsSelf<IService>(typeof(ServiceCollectionBuilder).Assembly);

        return services;
    }
}
