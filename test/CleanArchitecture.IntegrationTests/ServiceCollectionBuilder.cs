using CleanArchitecture.Configurations;
using CleanArchitecture.ServicesConfigurations;
using Framework.DependencyInjection.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.IntegrationTests;

public static class ServiceCollectionBuilder
{
    public static IServiceCollection Build(out IConfiguration configuration)
    {
        var configurationBuilder = new ConfigurationBuilder();
        Configuration.SetEnvironment(ApplicationFlavor.Default, DeploymentStage.Staging);
        Configuration.ConfigureAppConfiguration(configurationBuilder);
        configuration = configurationBuilder.Build();

        var services = new ServiceCollection();
        Configuration.ConfigureServices(services, configuration);
        services.AddLogging(Configuration.ConfigureLogging);
        services.RegisterSelfTransientServices();

        return services;
    }
}
