namespace Framework.Mediator.DomainEvents;

internal sealed class DomainEventPublisher<TEvent>
    where TEvent : IDomainEvent
{
    private readonly IEnumerable<IDomainEventHandler<TEvent>> handlers;

    public DomainEventPublisher(IEnumerable<IDomainEventHandler<TEvent>> handlers)
    {
        this.handlers = handlers;
    }

    public async Task<Result<Empty>> Publish(TEvent @event, CancellationToken cancellationToken)
    {
        foreach (var handler in handlers)
        {
            var result = await handler.Handle(@event, cancellationToken);

            if (result.IsFailure)
            {
                return result;
            }
        }

        return Empty.Value;
    }
}