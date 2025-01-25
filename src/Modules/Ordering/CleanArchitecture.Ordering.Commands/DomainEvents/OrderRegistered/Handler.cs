using Framework.Mediator.DomainEvents;
using Framework.Results;

namespace CleanArchitecture.Ordering.Commands.DomainEvents.OrderRegistered;

internal sealed class Handler : IDomainEventHandler<Event>
{
    public async Task<Result<Empty>> Handle(Event @event, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return Empty.Value;
    }
}
