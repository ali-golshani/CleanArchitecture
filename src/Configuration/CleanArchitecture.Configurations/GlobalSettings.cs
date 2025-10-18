namespace CleanArchitecture.Configurations;

public static class GlobalSettings
{
    public static class Messaging
    {
        public static readonly MessagingSystem MessagingSystem = MessagingSystem.Cap;
    }

    public static class Database
    {
        public static class ColumnTypes
        {
            public const string Decimal = "decimal(18, 2)";
        }
    }
}
