namespace CleanArchitecture.Configurations;

public static class GlobalSettings
{
    public static class Messaging
    {
        public static readonly MessagingSystem MessagingSystem = MessagingSystem.MassTransit;
    }

    public static class Database
    {
        public static class ColumnTypes
        {
            public const string Decimal = "decimal(18, 2)";
        }
    }

    public static class Audit
    {
        public const int MaxLengthOfAuditTrailResponse = 100_000;
        public static readonly bool LogAllRequests = false;
        public static readonly TimeSpan QueryResponseTimeThreshold = TimeSpan.FromSeconds(1);
    }

    public static class IntegrationEvents
    {
        public const int MaximumNumberOfRetries = 5;
        public static readonly TimeSpan EventWaitingTimeout = TimeSpan.FromSeconds(30);
        public static readonly TimeSpan DelayOnError = TimeSpan.FromSeconds(5);
    }
}
