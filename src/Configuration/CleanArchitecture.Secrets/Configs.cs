using CleanArchitecture.Configurations;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.Secrets;

public static class Configs
{
    public static void Configure(IConfigurationBuilder configuration, IEnvironment environment)
    {
        configuration.AddEnvironmentVariables(EnvironmentVariables.EnvironmentVariablesPrefix);
        configuration.AddJsonStream(Secrets.AuthenticationStream(environment.SecretsConfiguration));
        configuration.AddJsonStream(Secrets.ConnectionStringsStream(environment.SecretsConfiguration));
    }
}