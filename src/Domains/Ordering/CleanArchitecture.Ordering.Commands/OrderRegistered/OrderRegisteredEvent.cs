using CleanArchitecture.Mediator;

namespace CleanArchitecture.Ordering.Commands.OrderRegistered;

internal sealed class OrderRegisteredEvent : IInternalEvent
{
    public required int OrderId { get; init; }
}
