namespace CleanArchitecture.Mediator;

internal sealed class InternalEventPublisher<TEvent>
    where TEvent : IInternalEvent
{
    private readonly IEnumerable<IInternalEventHandler<TEvent>> handlers;

    public InternalEventPublisher(IEnumerable<IInternalEventHandler<TEvent>> handlers)
    {
        this.handlers = handlers;
    }

    public async Task<Result<Empty>> Publish(TEvent @event)
    {
        foreach (var handler in handlers)
        {
            var result = await handler.Handle(@event);

            if (result.IsFailure)
            {
                return result;
            }
        }

        return Result<Empty>.Success(Empty.Value);
    }
}