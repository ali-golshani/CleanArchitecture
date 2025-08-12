using CleanArchitecture.Configurations;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.Secrets;

public static class SecretsConfigs
{
    public static void Configure(IConfigurationBuilder configuration, IEnvironment environment)
    {
        configuration.AddEnvironmentVariables(EnvironmentVariables.EnvironmentVariablesPrefix);
        configuration.AddJsonStream(SecretsStream(environment.SecretsMode()));
    }

    private static MemoryStream SecretsStream(SecretsMode mode)
    {
        var fileName = ConfigurationFileName(mode);
        var filePath = ConfigurationFilePath(fileName);
        var fileContent = Utility.FileContent(filePath);
        if (ShouldDecrypt(fileContent))
        {
            fileContent = EnvironmentVariables.TryDecrypt(fileContent);
        }
        return Utility.ToStream(fileContent);
    }

    private static string ConfigurationFileName(SecretsMode mode)
    {
        return mode switch
        {
            SecretsMode.Development => $"Secrets.Development.json",
            SecretsMode.Staging => $"Secrets.Staging.json",
            SecretsMode.Production => $"Secrets.Production.json",
            SecretsMode.DbMigrationProduction => $"Secrets.Production.DbMigration.json",
            _ => $"Secrets.{mode}.json",
        };
    }

    private static string ConfigurationFilePath(string fileName)
    {
        var directory = AppDomain.CurrentDomain.BaseDirectory;
        return Path.Combine(directory, "Secrets", fileName);
    }

    private static bool ShouldDecrypt(string fileContent)
    {
        return Utility.IsBase64Encoding(fileContent);
    }
}