using Framework.Mediator.DomainEvents;

namespace CleanArchitecture.Ordering.Commands.DomainEvents.OrderRegistered;

internal sealed class Event : IDomainEvent
{
    public required int OrderId { get; init; }
}
