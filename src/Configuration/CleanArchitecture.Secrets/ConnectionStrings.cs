using CleanArchitecture.Configurations;
using CleanArchitecture.Secrets.Exceptions;

namespace CleanArchitecture.Secrets;

internal class ConnectionStrings : Secrets
{
    public static readonly ConnectionStrings Instance = new();

    protected override string ConfigurationFileName(SecretsConfiguration configuration)
    {
        return configuration switch
        {
            SecretsConfiguration.Staging => "ConnectionStrings.Staging.txt",
            SecretsConfiguration.Production => "ConnectionStrings.Production.txt",
            SecretsConfiguration.Development => "ConnectionStrings.Development.txt",
            SecretsConfiguration.DbMigration => "ConnectionStrings.DbMigration.txt",
            _ => throw new InvalidSecretsConfigurationException(configuration),
        };

    }

    protected override bool ShouldDecrypt(SecretsConfiguration onfiguration)
    {
        return onfiguration switch
        {
            SecretsConfiguration.Staging => true,
            SecretsConfiguration.Production => true,
            SecretsConfiguration.DbMigration => true,
            SecretsConfiguration.Development => false,
            _ => false,
        };
    }
}