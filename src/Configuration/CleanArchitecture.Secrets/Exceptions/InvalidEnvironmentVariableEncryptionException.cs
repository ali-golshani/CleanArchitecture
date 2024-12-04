namespace CleanArchitecture.Secrets.Exceptions;

public class InvalidEnvironmentVariableEncryptionException : Exception
{
    public InvalidEnvironmentVariableEncryptionException(string variable)
    {
        Variable = variable;
    }

    public string Variable { get; }
    public override string Message => $"Invalid Environment Variable '{Variable}' Encryption!";

}
