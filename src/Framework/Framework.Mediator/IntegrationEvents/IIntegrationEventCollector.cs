namespace Framework.Mediator.IntegrationEvents;

public interface IIntegrationEventCollector
{
    void Add(IIntegrationEvent @event);
    IReadOnlyCollection<IIntegrationEvent> Drain();
}
