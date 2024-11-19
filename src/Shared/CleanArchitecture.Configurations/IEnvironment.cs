namespace CleanArchitecture.Configurations;

public interface IEnvironment
{
    EnvironmentMode EnvironmentMode { get; }
    OptionsConfiguration OptionsConfiguration { get; }
    SecretsConfiguration SecretsConfiguration { get; }
}
