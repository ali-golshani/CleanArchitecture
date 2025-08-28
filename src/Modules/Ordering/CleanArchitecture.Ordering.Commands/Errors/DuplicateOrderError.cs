using CleanArchitecture.Ordering.Commands.Resources;
using Framework.Results.Errors;

namespace CleanArchitecture.Ordering.Commands.Errors;

internal class DuplicateOrderError(int orderId) : DuplicateError(ErrorMessageBuilder.DuplicateOrder(orderId))
{
    public int OrderId { get; } = orderId;
}
