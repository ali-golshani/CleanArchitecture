using Framework.Exceptions;

namespace CleanArchitecture.Ordering.Domain.Exceptions;

public class SubmitApprovedOrderException(int orderId) : DomainException(Resources.ExceptionMessages.SubmitApprovedOrder)
{
    public int OrderId { get; } = orderId;
}
