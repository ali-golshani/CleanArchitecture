using Framework.Exceptions;

namespace CleanArchitecture.Ordering.Domain.Exceptions;

public class SubmitCanceledOrderException(int orderId) : DomainException(Resources.ExceptionMessages.SubmitCanceledOrder)
{
    public int OrderId { get; } = orderId;
}
