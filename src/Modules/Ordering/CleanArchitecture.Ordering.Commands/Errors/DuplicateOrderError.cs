using Framework.Results.Errors;

namespace CleanArchitecture.Ordering.Commands.Errors;

internal class DuplicateOrderError(int orderId) : DuplicateError(PersianDictionary.OrderDictionary.Order, orderId);
