using Framework.Mediator.DomainEvents;

namespace CleanArchitecture.Ordering.Commands.OrderRegisteredEvent;

internal sealed class Event : IDomainEvent
{
    public required int OrderId { get; init; }
}
