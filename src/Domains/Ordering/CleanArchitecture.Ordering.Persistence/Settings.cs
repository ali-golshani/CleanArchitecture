using CleanArchitecture.Configurations;

namespace CleanArchitecture.Ordering.Persistence;

public static class Settings
{
    public static class SchemaNames
    {
        public const string Ordering = "ordering";
        public const string Sequence = "sequence";
    }

    public static class TableNames
    {
        public const string Order = "Orders";
    }

    public static class SequenceNames
    {
        public const string OrderId = "OrderIds";
    }

    public static class ColumnTypes
    {
        public const string Decimal = GlobalSettings.Database.ColumnTypes.Decimal;
    }

    public static class StringLengths
    {
        public const int CustomerName = 128;
        public const int TrackingCode = 32;
        public const int CommodityName = 32;
    }
}