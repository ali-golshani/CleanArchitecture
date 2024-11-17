namespace Framework.Domain.IntegrationEvents;

public interface IIntegrationEvent
{
    long EventId { get; }
    public int PublishTryCount { get; }
    public IntegrationEventPublishStatus PublishStatus { get; }

    void Update(IntegrationEventPublishStatus publishStatus, int publishTryCount);
}
