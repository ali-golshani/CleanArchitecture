namespace Framework.Mediator.IntegrationEvents;

public interface IIntegrationEvent
{
    public string Topic { get; }
    bool FireAndForget { get; }
}
