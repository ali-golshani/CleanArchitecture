namespace Framework.Mediator.IntegrationEvents;

public interface IIntegrationEventCollector
{
    void Add<TEvent>(TEvent @event) where TEvent : IIntegrationEvent;
    IReadOnlyCollection<IIntegrationEventEnvelope> Drain();
}
