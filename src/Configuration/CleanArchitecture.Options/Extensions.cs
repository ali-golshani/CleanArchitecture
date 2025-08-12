using CleanArchitecture.Configurations;

namespace CleanArchitecture.Options;

internal static class Extensions
{
    public static OptionsMode OptionsMode(this IEnvironment environment)
    {
        if (environment.Application == ApplicationFlavor.DbMigration)
        {
            return Options.OptionsMode.DbMigration;
        }

        return environment.DeploymentStage switch
        {
            DeploymentStage.Production => Options.OptionsMode.Production,
            DeploymentStage.Staging => Options.OptionsMode.Staging,
            _ => Options.OptionsMode.Development,
        };
    }
}
