using Framework.Mediator.DomainEvents;
using Framework.Results;

namespace CleanArchitecture.Ordering.Commands.OrderRegisteredEvent;

internal sealed class Handler : IDomainEventHandler<Event>
{
    public async Task<Result<Empty>> Handle(Event @event)
    {
        await Task.CompletedTask;
        return Empty.Value;
    }
}
