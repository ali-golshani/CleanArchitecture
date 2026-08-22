using CleanArchitecture.Ordering.Commands.Resources;
using Framework.Results;

namespace CleanArchitecture.Ordering.Commands.Errors;

public sealed class OrderNotFoundError(int orderId) : Error(ErrorCodes.OrderNotFound, ErrorType.NotFound, ErrorMessageBuilder.OrderNotFound(orderId))
{
    public int OrderId { get; } = orderId;
}
