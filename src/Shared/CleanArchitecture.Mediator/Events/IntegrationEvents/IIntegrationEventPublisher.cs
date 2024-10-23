namespace CleanArchitecture.Mediator;

public interface IIntegrationEventPublisher
{
    Task Publish(IIntegrationEvent @event);
}