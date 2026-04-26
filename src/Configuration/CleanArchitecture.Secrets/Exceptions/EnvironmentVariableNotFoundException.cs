using Framework.Exceptions;

namespace CleanArchitecture.Secrets.Exceptions;

public class EnvironmentVariableNotFoundException : ConfigurationException
{
    public EnvironmentVariableNotFoundException(string variable)
        : base($"Environment Variable '{variable}' is not available!")
    {
        Variable = variable;
    }

    public string Variable { get; }
}
