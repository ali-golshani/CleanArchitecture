namespace CleanArchitecture.Configurations;

public static class SystemEnvironment
{
    private static EnvironmentOptions environment = EnvironmentOptions.Development;

    public static void SetAsProductionEnvironment()
    {
        environment = EnvironmentOptions.Production;
    }

    public static void SetAsStagingEnvironment()
    {
        environment = EnvironmentOptions.Staging;
    }

    public static void SetAsDbMigrationEnvironment()
    {
        environment = EnvironmentOptions.DbMigration;
    }

    public static IEnvironment Environment => environment;
}
