using CleanArchitecture.Configurations;
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
        configuration.AddEnvironmentVariables(Secrets.EnvironmentVariables.EnvironmentVariablesPrefix);
        configuration.AddJsonStream(Secrets.Authentication.ConfigurationStream(SecretsConfiguration.Staging));
        configuration.AddJsonStream(Secrets.ConnectionStrings.ConfigurationStream(SecretsConfiguration.Staging));
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
