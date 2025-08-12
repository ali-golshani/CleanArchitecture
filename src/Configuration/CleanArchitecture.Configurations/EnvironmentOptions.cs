namespace CleanArchitecture.Configurations;

internal sealed class EnvironmentOptions : IEnvironment
{
    internal EnvironmentOptions(ApplicationFlavor application, DeploymentStage deploymentStage)
    {
        Application = application;
        DeploymentStage = deploymentStage;
    }

    public ApplicationFlavor Application { get; }
    public DeploymentStage DeploymentStage { get; }
}
