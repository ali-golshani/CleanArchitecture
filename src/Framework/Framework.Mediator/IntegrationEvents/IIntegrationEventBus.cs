namespace Framework.Mediator.IntegrationEvents;

public interface IIntegrationEventBus
{
    ValueTask Post(IIntegrationEvent @event);
    IReadOnlyCollection<IIntegrationEvent> Events { get; }
}
