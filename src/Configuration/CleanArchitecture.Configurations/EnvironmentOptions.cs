namespace CleanArchitecture.Configurations;

internal class EnvironmentOptions : IEnvironment
{
    public static readonly EnvironmentOptions Development = new EnvironmentOptions
    (
        environmentMode: EnvironmentMode.Development,
        optionsMode: OptionsMode.Development,
        secretsMode: SecretsMode.Development
    );

    public static readonly EnvironmentOptions Staging = new EnvironmentOptions
    (
        environmentMode: EnvironmentMode.Staging,
        optionsMode: OptionsMode.Staging,
        secretsMode: SecretsMode.Staging
    );

    public static readonly EnvironmentOptions Production = new EnvironmentOptions
    (
        environmentMode: EnvironmentMode.Production,
        optionsMode: OptionsMode.Production,
        secretsMode: SecretsMode.Production
    );

    public static readonly EnvironmentOptions DbMigration = new EnvironmentOptions
    (
        environmentMode: EnvironmentMode.Development,
        optionsMode: OptionsMode.DbMigration,
        secretsMode: SecretsMode.DbMigration
    );

    private EnvironmentOptions(
        EnvironmentMode environmentMode,
        OptionsMode optionsMode,
        SecretsMode secretsMode)
    {
        EnvironmentMode = environmentMode;
        OptionsMode = optionsMode;
        SecretsMode = secretsMode;
    }

    public EnvironmentMode EnvironmentMode { get; }
    public OptionsMode OptionsMode { get; }
    public SecretsMode SecretsMode { get; }
}
