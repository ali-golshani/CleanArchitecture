namespace CleanArchitecture.Configurations;

public class GlobalOptions
{
    public static readonly GlobalOptions Debug = new GlobalOptions(EnvironmentMode.Development);
    public static readonly GlobalOptions Staging = new GlobalOptions(EnvironmentMode.Staging);
    public static readonly GlobalOptions Production = new GlobalOptions(EnvironmentMode.Production);

    private GlobalOptions(EnvironmentMode environmentMode)
    {
        EnvironmentMode = environmentMode;
    }

    public EnvironmentMode EnvironmentMode { get; }
}
