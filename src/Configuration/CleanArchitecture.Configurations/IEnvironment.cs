namespace CleanArchitecture.Configurations;

public interface IEnvironment
{
    ApplicationFlavor Application { get; }
    DeploymentStage DeploymentStage { get; }
}
