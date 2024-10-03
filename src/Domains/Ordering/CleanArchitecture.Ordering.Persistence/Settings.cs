namespace CleanArchitecture.Ordering.Persistence;

public static class Settings
{
    public static class SchemaNames
    {
        public const string Order = "Order";
        public const string Sequence = "Sequence";
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
        public const string Decimal = "decimal(18, 2)";
    }

    public static class StringLengths
    {
        public const int CustomerName = 128;
        public const int TrackingCode = 32;
        public const int CommodityName = 32;
    }
}