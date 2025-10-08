using CleanArchitecture.Configurations;
using Framework.Exceptions;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.ServicesConfigurations;

internal static class ConnectionStringProvider
{
    public static string CleanArchitectureConnectionString(this IConfiguration configuration)
    {
        return
            configuration.GetConnectionString(ConfigurationSections.ConnectionStrings.CleanArchitectureDb) ??
            throw new ConfigurationException("Connection String is not Set !");
    }
}