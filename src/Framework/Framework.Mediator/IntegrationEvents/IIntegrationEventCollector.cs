namespace Framework.Mediator.IntegrationEvents;

public interface IIntegrationEventCollector
{
    void Add<TEvent>(Func<IntegrationEventHeader, TEvent> eventFactory)
        where TEvent : IIntegrationEvent;

    IReadOnlyCollection<IIntegrationEvent> Drain();
}
