namespace CleanArchitecture.Mediator;

public interface IIntegrationEventBus
{
    ValueTask Post(IIntegrationEvent @event);
}
