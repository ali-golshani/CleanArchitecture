namespace CleanArchitecture.Secrets;

internal static class Secrets
{
    public static MemoryStream AuthenticationStream(SecretsMode mode)
    {
        var fileName = FileName(mode);
        return ConfigurationStream(fileName);

        static string FileName(SecretsMode mode)
        {
            return $"Authentication.{mode}.json";
        }
    }

    public static MemoryStream ConnectionStringsStream(SecretsMode mode)
    {
        var fileName = FileName(mode);
        return ConfigurationStream(fileName);

        static string FileName(SecretsMode mode)
        {
            return $"ConnectionStrings.{mode}.json";
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