using Framework.Exceptions;

namespace CleanArchitecture.Ordering.Domain.Exceptions;

public class SubmitApprovedOrderException(int orderId) : DomainException(Resources.ExceptionMessages.SubmitApprovedOrder)
{
    public int OrderId { get; } = orderId;

    public override IEnumerable<(string Name, object? Value)> LogProperties
    {
        get
        {
            yield return (nameof(OrderId), OrderId);
        }
    }
}
