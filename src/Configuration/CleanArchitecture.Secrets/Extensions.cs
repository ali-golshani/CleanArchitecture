using CleanArchitecture.Configurations;

namespace CleanArchitecture.Secrets;

internal static class Extensions
{
    public static SecretsMode SecretsMode(this IEnvironment environment)
    {
        return environment.DeploymentStage switch
        {
            DeploymentStage.Production when environment.Application == ApplicationFlavor.DbMigration => Secrets.SecretsMode.ProductionDbMigration,
            DeploymentStage.Production => Secrets.SecretsMode.Production,
            DeploymentStage.Staging => Secrets.SecretsMode.Staging,
            _ => Secrets.SecretsMode.Development,
        };
    }
}
