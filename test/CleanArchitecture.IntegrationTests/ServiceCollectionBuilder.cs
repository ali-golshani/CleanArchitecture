using CleanArchitecture.Configurations;
using CleanArchitecture.IntegrationTests.Services;
using CleanArchitecture.ServicesConfigurations;
using Framework.Mediator.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.IntegrationTests;

public static class ServiceCollectionBuilder
{
    private static readonly string AppPath = AppDomain.CurrentDomain.BaseDirectory;

    public static IServiceCollection Build(out IConfiguration configuration)
    {
        SystemEnvironment.SetAsStagingEnvironment();

        var configurationBuilder = new ConfigurationBuilder();
        var services = new ServiceCollection();

        configurationBuilder.SetBasePath(AppPath);

        Configuration.ConfigureAppConfiguration
        (
            configuration: configurationBuilder,
            environment: SystemEnvironment.Environment
        );

        configuration = configurationBuilder.Build();

        Configuration.ConfigureServices(services, configuration, SystemEnvironment.Environment);
        services.AddLogging(Configuration.ConfigureLogging);

        services.RegisterAsSelf<IService>();

        return services;
    }
}
