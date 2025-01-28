using CleanArchitecture.Configurations;

namespace CleanArchitecture.Options;

public static class Options
{
    public static string ConfigurationFile(OptionsConfiguration configuration)
    {
        return Path.Combine("Options", $"Options.{configuration}.json");
    }

    public static string[] DeleteConfigurationFiles(OptionsConfiguration configuration)
    {
        OptionsConfiguration[] configurations =
        [
            OptionsConfiguration.Development,
            OptionsConfiguration.Production,
            OptionsConfiguration.Staging,
            OptionsConfiguration.DbMigration,
        ];

        return configurations.Where(x => x != configuration).Select(ConfigurationFile).ToArray();
    }
}