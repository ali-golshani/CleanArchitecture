using CleanArchitecture.Configurations;

namespace CleanArchitecture.Options;

public static class Options
{
    public static Stream ConfigurationStream(OptionsConfiguration configuration)
    {
        var data = ConfigurationData(configuration);
        return new MemoryStream(data);
    }

    private static byte[] ConfigurationData(OptionsConfiguration configuration)
    {
        return configuration switch
        {
            OptionsConfiguration.Production => Properties.Resources.Production,
            OptionsConfiguration.Staging => Properties.Resources.Staging,
            OptionsConfiguration.DbMigration => Properties.Resources.DbMigration,
            _ => Properties.Resources.Development,
        };
    }
}