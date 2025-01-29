using CleanArchitecture.Configurations;
using CleanArchitecture.Secrets.Exceptions;

namespace CleanArchitecture.Secrets;

internal static class Secrets
{
    public static MemoryStream AuthenticationStream(SecretsConfiguration configuration)
    {
        var fileName = AuthenticationFileName(configuration);
        return ConfigurationStream(fileName);
    }

    public static MemoryStream ConnectionStringsStream(SecretsConfiguration configuration)
    {
        var fileName = ConnectionStringsFileName(configuration);
        return ConfigurationStream(fileName);
    }

    private static MemoryStream ConfigurationStream(string fileName)
    {
        var filePath = ConfigurationFilePath(fileName);
        var fileContent = Utility.FileContent(filePath);
        if (ShouldDecrypt(fileContent))
        {
            fileContent = EnvironmentVariables.TryDecrypt(fileContent);
        }
        return Utility.ToStream(fileContent);
    }

    private static string ConfigurationFilePath(string fileName)
    {
        var directory = AppDomain.CurrentDomain.BaseDirectory;
        return Path.Combine(directory, "Secrets", fileName);
    }

    private static string AuthenticationFileName(SecretsConfiguration configuration)
    {
        return configuration switch
        {
            SecretsConfiguration.Staging => "Authentication.Staging.txt",
            SecretsConfiguration.Production => "Authentication.Production.txt",
            SecretsConfiguration.Development => "Authentication.Development.json",
            SecretsConfiguration.DbMigration => "Authentication.DbMigration.json",
            _ => throw new InvalidSecretsConfigurationException(configuration),
        };
    }

    private static string ConnectionStringsFileName(SecretsConfiguration configuration)
    {
        return configuration switch
        {
            SecretsConfiguration.Staging => "ConnectionStrings.Staging.txt",
            SecretsConfiguration.Production => "ConnectionStrings.Production.txt",
            SecretsConfiguration.Development => "ConnectionStrings.Development.json",
            SecretsConfiguration.DbMigration => "ConnectionStrings.DbMigration.txt",
            _ => throw new InvalidSecretsConfigurationException(configuration),
        };
    }

    private static bool ShouldDecrypt(string fileContent)
    {
        return Utility.IsBase64Encoding(fileContent);
    }
}