using CleanArchitecture.Configurations;
using CleanArchitecture.Secrets.Exceptions;

namespace CleanArchitecture.Secrets;

internal static class Secrets
{
    public static MemoryStream AuthenticationStream(SecretsConfiguration configuration)
    {
        var fileName = FileName(configuration);
        return ConfigurationStream(fileName);

        static string FileName(SecretsConfiguration configuration)
        {
            return $"Authentication.{configuration}.json";
        }
    }

    public static MemoryStream ConnectionStringsStream(SecretsConfiguration configuration)
    {
        var fileName = FileName(configuration);
        return ConfigurationStream(fileName);

        static string FileName(SecretsConfiguration configuration)
        {
            return $"ConnectionStrings.{configuration}.json";
        }
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

    private static bool ShouldDecrypt(string fileContent)
    {
        return Utility.IsBase64Encoding(fileContent);
    }
}