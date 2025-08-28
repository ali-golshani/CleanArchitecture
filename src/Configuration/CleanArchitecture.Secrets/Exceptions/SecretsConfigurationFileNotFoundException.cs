using Framework.Exceptions;

namespace CleanArchitecture.Secrets.Exceptions;

public class SecretsConfigurationFileNotFoundException : ConfigurationException
{
    public SecretsConfigurationFileNotFoundException(string filePath)
        : base($"Secrets Configuration File '{filePath}' is not exists!")
    {
        FilePath = filePath;
    }

    public string FilePath { get; }
}
