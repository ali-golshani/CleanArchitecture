using CleanArchitecture.Configurations;
using CleanArchitecture.Secrets.Exceptions;

namespace CleanArchitecture.Secrets;

internal abstract class SecretsBase
{
    protected abstract bool ShouldDecrypt(SecretsConfiguration onfiguration);
    protected abstract string ConfigurationFileName(SecretsConfiguration configuration);

    public MemoryStream ConfigurationStream(SecretsConfiguration configuration)
    {
        var text = ConfigurationString(configuration);
        if (ShouldDecrypt(configuration))
        {
            text = EnvironmentVariables.TryDecrypt(text);
        }
        return ToStream(text);
    }

    protected string ConfigurationString(SecretsConfiguration configuration)
    {
        var filePath = ConfigurationFilePath(configuration);

        if (!File.Exists(filePath))
        {
            throw new SecretsConfigurationFileNotFoundException(filePath);
        }

        return File.ReadAllText(filePath);
    }

    protected string ConfigurationFilePath(SecretsConfiguration configuration)
    {
        var fileName = ConfigurationFileName(configuration);
        var directory = AppDomain.CurrentDomain.BaseDirectory;
        return Path.Combine(directory, "Secrets", fileName);
    }

    protected static MemoryStream ToStream(string text)
    {
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(text);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }
}