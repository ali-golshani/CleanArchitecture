namespace Framework.Mediator.IntegrationEvents;

public interface IIntegrationEvent
{
    string Topic { get; }
    IntegrationEventHeader Header { get; }
}
