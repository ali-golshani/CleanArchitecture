namespace CleanArchitecture.Configurations;

public static class GlobalSettings
{
    private static GlobalOptions options = GlobalOptions.Debug;

    public static void SetGlobalOptions(EnvironmentMode environmentMode)
    {
        switch (environmentMode)
        {
            case EnvironmentMode.Production:
                options = GlobalOptions.Production;
                break;

            case EnvironmentMode.Staging:
                options = GlobalOptions.Staging;
                break;

            default:
                options = GlobalOptions.Debug;
                break;
        }
    }

    public static EnvironmentMode Environment => options.EnvironmentMode;

    public static class Database
    {
        public static class SchemaNames
        {
            public const string Audit_Schema = "Audit";
        }
    }

    public static class Logging
    {
        public const int MaxLengthOfLogEntryResponse = 100_000;
        public const bool LogSuccessQuery = false;
        public static readonly TimeSpan QueryResponseTimeThreshold = TimeSpan.FromSeconds(1);
    }

    public static class DomainEvents
    {
        public const int MaximumNumberOfRetries = 5;
        public static readonly TimeSpan EventWaitingTimeout = TimeSpan.FromSeconds(30);
        public static readonly TimeSpan DelayOnError = TimeSpan.FromSeconds(5);
    }
}
