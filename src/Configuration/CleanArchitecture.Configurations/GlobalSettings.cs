namespace CleanArchitecture.Configurations;

public static class GlobalSettings
{
    public static class Messaging
    {
        /// <summary>
        /// MassTransit v9 (and beyond) requires a license to use
        /// </summary>
        public static readonly bool SupportMassTransit = false;

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
