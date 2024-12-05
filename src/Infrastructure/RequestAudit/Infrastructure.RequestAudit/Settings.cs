namespace Infrastructure.RequestAudit;

public static class Settings
{
    public const int MaxLengthOfAuditTrailResponse = 100_000;
    public static readonly bool LogAllRequests = false;
    public static readonly TimeSpan QueryResponseTimeThreshold = TimeSpan.FromSeconds(1);

    public static class Persistence
    {
        public static class SchemaNames
        {
            public const string Audit = "audit";
        }

        public static class ColumnTypes
        {
            public const string Decimal = GlobalSettings.Database.ColumnTypes.Decimal;
        }
    }
}