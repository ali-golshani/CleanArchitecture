using CleanArchitecture.Configurations;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.Options;

public static class OptionsConfigs
{
    public static void Configure(IConfigurationBuilder configuration, IEnvironment environment)
    {
        var fileName = ConfigurationFileName(environment.OptionsMode());
        var filePath = ConfigurationFilePath(fileName);
        configuration.AddJsonFile(filePath, optional: false, reloadOnChange: true);
    }

    private static string ConfigurationFileName(OptionsMode mode)
    {
        return mode switch
        {
            OptionsMode.Development => $"Options.Development.json",
            OptionsMode.Staging => $"Options.Staging.json",
            OptionsMode.Production => $"Options.Production.json",
            OptionsMode.ProductionDbMigration => $"Options.Production.DbMigration.json",
            _ => $"Options.{mode}.json",
        };
    }

    private static string ConfigurationFilePath(string fileName)
    {
        var directory = AppDomain.CurrentDomain.BaseDirectory;
        return Path.Combine(directory, "Options", fileName);
    }
}