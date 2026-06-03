using Framework.Exceptions;

namespace CleanArchitecture.Ordering.Domain.Exceptions;

public class InvalidOrderStatusException(int orderId) : ProgrammerException
{
    public int OrderId { get; } = orderId;
    public override string Message => Resources.ExceptionMessages.InvalidOrderStatus;

    public override IEnumerable<Fact> Facts
    {
        get
        {
            yield return new(nameof(TechnicalMessage), TechnicalMessage);
            yield return new(nameof(OrderId), OrderId);
        }
    }
}
