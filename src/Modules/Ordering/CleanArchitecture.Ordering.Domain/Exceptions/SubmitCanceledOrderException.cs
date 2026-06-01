using Framework.Exceptions;

namespace CleanArchitecture.Ordering.Domain.Exceptions;

public class SubmitCanceledOrderException(int orderId) : DomainException(Resources.ExceptionMessages.SubmitCanceledOrder)
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
