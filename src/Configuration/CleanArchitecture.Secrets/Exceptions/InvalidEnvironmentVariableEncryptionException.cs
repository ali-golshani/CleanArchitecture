using Framework.Exceptions;

namespace CleanArchitecture.Secrets.Exceptions;

public class InvalidEnvironmentVariableEncryptionException : ConfigurationException
{
    public InvalidEnvironmentVariableEncryptionException(string variable)
        : base($"Invalid Environment Variable '{variable}' Encryption!")
    {
        Variable = variable;
    }

    public string Variable { get; }
}
