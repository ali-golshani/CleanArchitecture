using CleanArchitecture.Configurations;

namespace CleanArchitecture.Secrets.Exceptions;

public class InvalidSecretsConfigurationException : Exception
{
    public InvalidSecretsConfigurationException(SecretsConfiguration secretsConfiguration)
    {
        SecretsConfiguration = secretsConfiguration;
    }

    public SecretsConfiguration SecretsConfiguration { get; }

    public override string Message
    {
        get
        {
            return $"Secrets Configuration value ({(int)SecretsConfiguration}) is not valid";
        }
    }
}
