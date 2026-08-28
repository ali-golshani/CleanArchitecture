namespace Framework.Mediator.IntegrationEvents;

public interface IIntegrationEvent
{
    static abstract string Topic { get; }
}
