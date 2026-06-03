using Framework.Exceptions;

namespace CleanArchitecture.Ordering.Domain.Exceptions;

public class SubmitCanceledOrderException(int orderId) : DomainException(Resources.ExceptionMessages.SubmitCanceledOrder)
{
    public int OrderId { get; } = orderId;

    public override IEnumerable<Fact> Facts
    {
        get
        {
            yield return new(nameof(OrderId), OrderId);
        }
    }
}
