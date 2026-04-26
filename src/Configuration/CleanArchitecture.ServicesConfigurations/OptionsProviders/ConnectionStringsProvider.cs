using CleanArchitecture.Configurations;
using Framework.Exceptions;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.ServicesConfigurations.OptionsProviders;

internal static class ConnectionStringsProvider
{
    public static ConnectionStrings ConnectionStrings(this IConfiguration configuration)
    {
        return new(CleanArchitectureConnectionString(configuration));
    }

    private static string CleanArchitectureConnectionString(IConfiguration configuration)
    {
        return
            configuration.GetConnectionString(ConfigurationSections.ConnectionStrings.CleanArchitectureDb) ??
            throw new ConfigurationException("Connection String is not Set !");
    }
}