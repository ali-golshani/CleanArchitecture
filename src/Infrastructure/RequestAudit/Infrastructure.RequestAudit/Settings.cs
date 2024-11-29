namespace Infrastructure.RequestAudit;

public static class Settings
{
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