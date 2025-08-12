namespace CleanArchitecture.Configurations;

public interface IEnvironment
{
    DeploymentStage DeploymentStage { get; }
    OptionsMode OptionsMode { get; }
    SecretsMode SecretsMode { get; }
}
