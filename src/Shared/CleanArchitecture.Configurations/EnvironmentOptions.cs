namespace CleanArchitecture.Configurations;

public class EnvironmentOptions
{
    public static readonly EnvironmentOptions Development = new EnvironmentOptions
    (
        environmentMode: EnvironmentMode.Development,
        optionsConfiguration: OptionsConfiguration.Development,
        secretsConfiguration: SecretsConfiguration.Development
    );

    public static readonly EnvironmentOptions Staging = new EnvironmentOptions
    (
        environmentMode: EnvironmentMode.Staging,
        optionsConfiguration: OptionsConfiguration.Staging,
        secretsConfiguration: SecretsConfiguration.Staging
    );

    public static readonly EnvironmentOptions Production = new EnvironmentOptions
    (
        environmentMode: EnvironmentMode.Production,
        optionsConfiguration: OptionsConfiguration.Production,
        secretsConfiguration: SecretsConfiguration.Production
    );

    public static readonly EnvironmentOptions DbMigration = new EnvironmentOptions
    (
        environmentMode: EnvironmentMode.Development,
        optionsConfiguration: OptionsConfiguration.DbMigration,
        secretsConfiguration: SecretsConfiguration.DbMigration
    );

    private EnvironmentOptions(
        EnvironmentMode environmentMode,
        OptionsConfiguration optionsConfiguration,
        SecretsConfiguration secretsConfiguration)
    {
        EnvironmentMode = environmentMode;
        OptionsConfiguration = optionsConfiguration;
        SecretsConfiguration = secretsConfiguration;
    }

    public EnvironmentMode EnvironmentMode { get; }
    public OptionsConfiguration OptionsConfiguration { get; }
    public SecretsConfiguration SecretsConfiguration { get; set; }
}
