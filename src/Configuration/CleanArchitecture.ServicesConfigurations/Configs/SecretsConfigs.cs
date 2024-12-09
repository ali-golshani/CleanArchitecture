using CleanArchitecture.Configurations;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.ServicesConfigurations.Configs;

internal static class SecretsConfigs
{
    public static void ConfigureSecrets(IConfigurationBuilder configuration, IEnvironment environment)
    {
        configuration.AddEnvironmentVariables(Secrets.EnvironmentVariables.EnvironmentVariablesPrefix);
        configuration.AddJsonStream(Secrets.Authentication.ConfigurationStream(environment.SecretsConfiguration));
        configuration.AddJsonStream(Secrets.ConnectionStrings.ConfigurationStream(environment.SecretsConfiguration));
    }
}
