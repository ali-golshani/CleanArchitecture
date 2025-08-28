using CleanArchitecture.Ordering.Commands.Resources;
using Framework.Results.Errors;

namespace CleanArchitecture.Ordering.Commands.Errors;

public class OrderNotFoundError(int orderId) : NotFoundError(ErrorMessageBuilder.OrderNotFound(orderId))
{
    public int OrderId { get; } = orderId;
}
