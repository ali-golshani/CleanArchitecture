namespace CleanArchitecture.Secrets.Exceptions;

public class EnvironmentVariableNotFoundException : Exception
{
    public EnvironmentVariableNotFoundException(string variable)
    {
        Variable = variable;
    }

    public string Variable { get; }
    public override string Message => $"Environment Variable '{Variable}' is not available!";

}
