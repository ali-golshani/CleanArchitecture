using CleanArchitecture.Configurations;

namespace CleanArchitecture.Secrets.Exceptions;

public class InvalidSecretsConfigurationException : Exception
{
    public InvalidSecretsConfigurationException(SecretsMode secretsMode)
    {
        SecretsMode = secretsMode;
    }

    public SecretsMode SecretsMode { get; }

    public override string Message
    {
        get
        {
            return $"Secrets Mode value ({(int)SecretsMode}) is not valid";
        }
    }
}
