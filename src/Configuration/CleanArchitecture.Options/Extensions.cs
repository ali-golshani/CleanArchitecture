using CleanArchitecture.Configurations;

namespace CleanArchitecture.Options;

internal static class Extensions
{
    public static OptionsMode OptionsMode(this IEnvironment environment)
    {
        return environment.DeploymentStage switch
        {
            DeploymentStage.Production when environment.Application == ApplicationFlavor.DbMigration => Options.OptionsMode.ProductionDbMigration,
            DeploymentStage.Production => Options.OptionsMode.Production,
            DeploymentStage.Staging => Options.OptionsMode.Staging,
            _ => Options.OptionsMode.Development,
        };
    }
}
