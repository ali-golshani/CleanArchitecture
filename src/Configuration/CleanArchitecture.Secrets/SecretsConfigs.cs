using CleanArchitecture.Configurations;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.Secrets;

public static class SecretsConfigs
{
    public static void Configure(IConfigurationBuilder configuration, IEnvironment environment)
    {
        configuration.AddEnvironmentVariables(EnvironmentVariables.EnvironmentVariablesPrefix);
        configuration.AddJsonStream(Secrets.AuthenticationStream(environment.SecretsMode()));
        configuration.AddJsonStream(Secrets.ConnectionStringsStream(environment.SecretsMode()));
    }
}