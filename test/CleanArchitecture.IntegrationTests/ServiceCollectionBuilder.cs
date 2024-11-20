using CleanArchitecture.Configurations;
using CleanArchitecture.IntegrationTests.Services;
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

        RegisterTestServices(services);

        return services;
    }

    private static void RegisterTestServices(IServiceCollection services)
    {
        services.Scan(scan =>
        {
            scan
                .FromAssemblyOf<ITestService>()
                .AddClasses(classes => classes.AssignableTo<ITestService>())
                .AsSelf()
                .WithTransientLifetime()
                ;
        });
    }
}
