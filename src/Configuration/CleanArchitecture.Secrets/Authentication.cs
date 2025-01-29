using CleanArchitecture.Configurations;
using CleanArchitecture.Secrets.Exceptions;

namespace CleanArchitecture.Secrets;

internal class Authentication : Secrets
{
    public static readonly Authentication Instance = new();

    protected override string ConfigurationFileName(SecretsConfiguration configuration)
    {
        return configuration switch
        {
            SecretsConfiguration.Staging => "Authentication.Staging.txt",
            SecretsConfiguration.Production => "Authentication.Production.txt",
            SecretsConfiguration.Development => "Authentication.Development.json",
            SecretsConfiguration.DbMigration => "Authentication.DbMigration.json",
            _ => throw new InvalidSecretsConfigurationException(configuration),
        };

    }

    protected override bool ShouldDecrypt(SecretsConfiguration onfiguration)
    {
        return onfiguration switch
        {
            SecretsConfiguration.Staging => true,
            SecretsConfiguration.Production => true,
            SecretsConfiguration.DbMigration => false,
            SecretsConfiguration.Development => false,
            _ => false,
        };
    }
}