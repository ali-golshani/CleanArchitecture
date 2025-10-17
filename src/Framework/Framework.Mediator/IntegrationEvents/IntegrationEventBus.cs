namespace Framework.Mediator.IntegrationEvents;

public class IntegrationEventBus : IIntegrationEventBus
{
    private readonly Queue<IIntegrationEvent> events = [];

    public IReadOnlyCollection<IIntegrationEvent> Events => [.. events];

    public ValueTask Post(IIntegrationEvent @event)
    {
        events.Enqueue(@event);
        return ValueTask.CompletedTask;
    }
}
