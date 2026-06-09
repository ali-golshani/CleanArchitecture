using CleanArchitecture.Ordering.Commands.Resources;
using Framework.Results;

namespace CleanArchitecture.Ordering.Commands.Errors;

public sealed class DuplicateOrderError(int orderId) : Error(ErrorType.Conflict, ErrorMessageBuilder.DuplicateOrder(orderId))
{
    public int OrderId { get; } = orderId;
}
