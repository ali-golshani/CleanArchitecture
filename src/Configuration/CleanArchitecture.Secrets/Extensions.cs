using CleanArchitecture.Configurations;

namespace CleanArchitecture.Secrets;

internal static class Extensions
{
    public static SecretsMode SecretsMode(this IEnvironment environment)
    {
        if (environment.DeploymentStage == DeploymentStage.Production)
        {
            return environment.Application switch
            {
                ApplicationFlavor.DbMigration => Secrets.SecretsMode.DbMigrationProduction,
                _ => Secrets.SecretsMode.Production,
            };
        }

        if (environment.DeploymentStage == DeploymentStage.Staging)
        {
            return Secrets.SecretsMode.Staging;
        }

        return Secrets.SecretsMode.Development;
    }
}
