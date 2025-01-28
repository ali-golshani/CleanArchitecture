using CleanArchitecture.Configurations;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.ServicesConfigurations.Configs;

internal static class OptionsConfigs
{
    public static void ConfigureOptions(IConfigurationBuilder configuration, IEnvironment environment)
    {
        var directory = AppDomain.CurrentDomain.BaseDirectory;
        var fileName = Options.Options.ConfigurationFile(environment.OptionsConfiguration);
        var filePath = Path.Combine(directory, fileName);
        configuration.AddJsonFile(filePath, optional: false, reloadOnChange: true);
    }
}
