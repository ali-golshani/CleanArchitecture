namespace CleanArchitecture.Configurations;

public static class SystemEnvironment
{
    private static EnvironmentOptions environment = new(ApplicationFlavor.Default, DeploymentStage.Development);

    public static void SetEnvironment(ApplicationFlavor application, DeploymentStage stage)
    {
        environment = new(application, stage);
    }

    public static IEnvironment Environment => environment;
}
