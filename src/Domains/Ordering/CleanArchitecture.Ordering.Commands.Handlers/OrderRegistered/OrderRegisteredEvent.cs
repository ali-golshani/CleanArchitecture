using CleanArchitecture.Mediator;

namespace CleanArchitecture.Ordering.Commands.OrderRegistered;

internal sealed class OrderRegisteredEvent : IDomainEvent
{
    public required int OrderId { get; init; }
}
