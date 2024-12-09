using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.ServicesConfigurations.Configs;

internal static class GlobalSettingsConfigs
{
    public static void RegisterSettings(IConfigurationBuilder configuration)
    {
        var path = AppDomain.CurrentDomain.BaseDirectory;
        var configFileName = $"GlobalSettings.json";
        configuration.AddJsonFile($"{path}{configFileName}", optional: false, reloadOnChange: true);
    }
}
