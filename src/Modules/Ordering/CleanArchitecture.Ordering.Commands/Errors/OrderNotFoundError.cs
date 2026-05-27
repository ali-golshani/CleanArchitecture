using CleanArchitecture.Ordering.Commands.Resources;
using Framework.Results;

namespace CleanArchitecture.Ordering.Commands.Errors;

public class OrderNotFoundError(int orderId) : Error(ErrorType.NotFound, ErrorMessageBuilder.OrderNotFound(orderId))
{
    public int OrderId { get; } = orderId;
}
