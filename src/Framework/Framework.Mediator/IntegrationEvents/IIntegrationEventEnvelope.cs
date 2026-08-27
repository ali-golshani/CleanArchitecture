namespace Framework.Mediator.IntegrationEvents;

public interface IIntegrationEventEnvelope
{
    Guid MessageId { get; }
    Guid CorrelationId { get; }
    string Topic { get; }
}
