namespace CleanArchitecture.Ordering.Commands.Resources
{
    internal static class ErrorMessageBuilder
    {
        public static string OrderNotFound(int orderId)
        {
            return string.Format(ErrorMessages.OrderNotFoundError, orderId);
        }

        public static string CommodityNotFound(int commodityId)
        {
            return string.Format(ErrorMessages.CommodityNotFoundError, commodityId);
        }

        public static string DuplicateOrder(int orderId)
        {
            return string.Format(ErrorMessages.DuplicateOrderError, orderId);
        }
    }
}
