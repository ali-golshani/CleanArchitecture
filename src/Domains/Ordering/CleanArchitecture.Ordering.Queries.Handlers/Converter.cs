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
        };
    }
}
