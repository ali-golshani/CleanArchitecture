namespace CleanArchitecture.Configurations;

internal class EnvironmentOptions : IEnvironment
{
    public static readonly EnvironmentOptions Development = new EnvironmentOptions
    (
        deploymentStage: DeploymentStage.Development,
        optionsMode: OptionsMode.Development,
        secretsMode: SecretsMode.Development
    );

    public static readonly EnvironmentOptions Staging = new EnvironmentOptions
    (
        deploymentStage: DeploymentStage.Staging,
        optionsMode: OptionsMode.Staging,
        secretsMode: SecretsMode.Staging
    );

    public static readonly EnvironmentOptions Production = new EnvironmentOptions
    (
        deploymentStage: DeploymentStage.Production,
        optionsMode: OptionsMode.Production,
        secretsMode: SecretsMode.Production
    );

    public static readonly EnvironmentOptions DbMigration = new EnvironmentOptions
    (
        deploymentStage: DeploymentStage.Development,
        optionsMode: OptionsMode.DbMigration,
        secretsMode: SecretsMode.DbMigration
    );

    private EnvironmentOptions(
        DeploymentStage deploymentStage,
        OptionsMode optionsMode,
        SecretsMode secretsMode)
    {
        DeploymentStage = deploymentStage;
        OptionsMode = optionsMode;
        SecretsMode = secretsMode;
    }

    public DeploymentStage DeploymentStage { get; }
    public OptionsMode OptionsMode { get; }
    public SecretsMode SecretsMode { get; }
}
