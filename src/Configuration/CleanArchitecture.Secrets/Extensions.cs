using CleanArchitecture.Configurations;

namespace CleanArchitecture.Secrets;

internal static class Extensions
{
    public static SecretsMode SecretsMode(this IEnvironment environment)
    {
        if (environment.Application == ApplicationFlavor.DbMigration)
        {
            return CleanArchitecture.Secrets.SecretsMode.DbMigration;
        }

        return environment.DeploymentStage switch
        {
            DeploymentStage.Production => CleanArchitecture.Secrets.SecretsMode.Production,
            DeploymentStage.Staging => CleanArchitecture.Secrets.SecretsMode.Staging,
            _ => CleanArchitecture.Secrets.SecretsMode.Development,
        };
    }
}
