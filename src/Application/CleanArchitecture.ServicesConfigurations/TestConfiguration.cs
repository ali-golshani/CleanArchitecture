using CleanArchitecture.Configurations;
using CleanArchitecture.Secrets;
using CleanArchitecture.ServicesConfigurations.Configs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.ServicesConfigurations;

public static class TestConfiguration
{
    public static void ConfigureAppConfiguration(
        IConfigurationBuilder configuration,
        OptionsConfiguration optionsConfiguration)
    {
        configuration.Sources.Clear();
        GlobalConfigs.RegisterSettings(configuration);
        configuration.AddJsonStream(Options.Options.ConfigurationStream(optionsConfiguration));
        configuration.AddEnvironmentVariables(EnvironmentVariables.EnvironmentVariablesPrefix);
        configuration.AddJsonStream(Secrets.Secrets.ConfigurationStream(SecretsConfiguration.Staging));
    }

    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        Configuration.RegisterServices
        (
            services: services,
            configuration: configuration
        );
    }
}
