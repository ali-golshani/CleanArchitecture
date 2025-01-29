namespace CleanArchitecture.Secrets.Exceptions;

public class SecretsConfigurationFileNotFoundException : Exception
{
    public SecretsConfigurationFileNotFoundException(string filePath)
    {
        FilePath = filePath;
    }

    public string FilePath { get; }
    public override string Message => $"Secrets Configuration File '{FilePath}' is not exists!";

}
