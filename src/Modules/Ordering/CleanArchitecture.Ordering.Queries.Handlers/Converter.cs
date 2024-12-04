namespace CleanArchitecture.Ordering.Queries;

internal static class Converter
{
    public static Models.Order Convert(this Domain.Order order)
    {
        return new Models.Order
        {
            OrderId = order.OrderId,
            Price = order.Price,
            Quantity = order.Quantity,
            BrokerId = order.BrokerId,
            CustomerId = order.CustomerId,
            Status = order.Status,
            TrackingCode = order.TrackingCode,
            Commodity = new Models.Commodity
            {
                CommodityId = order.Commodity.CommodityId,
                CommodityName = order.Commodity.CommodityName,
            }
        };
    }
}
