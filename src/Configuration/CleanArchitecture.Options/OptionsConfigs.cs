using CleanArchitecture.Configurations;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.Options;

public static class OptionsConfigs
{
    public static void Configure(IConfigurationBuilder configuration, IEnvironment environment)
    {
        var directory = AppDomain.CurrentDomain.BaseDirectory;
        var fileName = ConfigurationFile(environment.OptionsMode());
        var filePath = Path.Combine(directory, fileName);
        configuration.AddJsonFile(filePath, optional: false, reloadOnChange: true);
    }

    private static string ConfigurationFile(OptionsMode mode)
    {
        return Path.Combine("Options", $"Options.{mode}.json");
    }
}