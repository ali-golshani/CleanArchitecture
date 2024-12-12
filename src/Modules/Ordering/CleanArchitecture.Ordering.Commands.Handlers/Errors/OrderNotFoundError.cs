using Framework.Results.Errors;

namespace CleanArchitecture.Ordering.Commands.Errors;

public class OrderNotFoundError(int orderId) : NotFoundError(PersianDictionary.OrderDictionary.Order, orderId);
