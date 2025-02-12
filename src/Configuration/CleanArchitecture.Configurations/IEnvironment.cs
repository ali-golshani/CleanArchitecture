namespace CleanArchitecture.Configurations;

public interface IEnvironment
{
    EnvironmentMode EnvironmentMode { get; }
    OptionsMode OptionsMode { get; }
    SecretsMode SecretsMode { get; }
}
