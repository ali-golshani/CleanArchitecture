namespace CleanArchitecture.Configurations;

public static class SystemEnvironment
{
    private static EnvironmentOptions environment = EnvironmentOptions.Development;

    public static void SetProductionEnvironment()
    {
        environment = EnvironmentOptions.Production;
    }

    public static void SetStagingEnvironment()
    {
        environment = EnvironmentOptions.Staging;
    }

    public static void SetDbMigrationEnvironment()
    {
        environment = EnvironmentOptions.DbMigration;
    }

    public static IEnvironment Environment => environment;
}
