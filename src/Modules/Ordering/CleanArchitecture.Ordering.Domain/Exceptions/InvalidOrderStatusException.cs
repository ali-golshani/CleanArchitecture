using Framework.Exceptions;

namespace CleanArchitecture.Ordering.Domain.Exceptions;

public class InvalidOrderStatusException(int orderId) : ProgrammerException
{
    public int OrderId { get; } = orderId;
    public override string Message => Resources.ExceptionMessages.InvalidOrderStatus;

    public override IEnumerable<(string Name, object? Value)> LogProperties
    {
        get
        {
            yield return (nameof(TechnicalMessage), TechnicalMessage);
            yield return (nameof(OrderId), OrderId);
        }
    }
}
