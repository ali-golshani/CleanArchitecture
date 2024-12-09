using CleanArchitecture.Configurations;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.ServicesConfigurations.Configs;

internal static class OptionsConfigs
{
    public static void ConfigureOptions(IConfigurationBuilder configuration, IEnvironment environment)
    {
        var directory = AppDomain.CurrentDomain.BaseDirectory;
        var filePath = Path.Combine(directory, Settings.OptionsFileName);
        var bytes = Options.Options.ConfigurationData(environment.OptionsConfiguration);
        File.WriteAllBytes(filePath, bytes);
        configuration.AddJsonFile(filePath, optional: false, reloadOnChange: true);
    }
}
