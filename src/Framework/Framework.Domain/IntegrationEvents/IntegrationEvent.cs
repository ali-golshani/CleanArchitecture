namespace Framework.Domain.IntegrationEvents;

public abstract class IntegrationEvent : IIntegrationEvent
{
    public long EventId { get; }
    public Guid EventGuid { get; }
    public DateTime EventTime { get; }
    public IntegrationEventPublishStatus PublishStatus { get; private set; }
    public int PublishTryCount { get; private set; }
    public Guid? CorrelationId { get; set; }

    protected IntegrationEvent(DateTime eventTime)
    {
        EventGuid = Guid.NewGuid();
        PublishStatus = IntegrationEventPublishStatus.InProcess;
        PublishTryCount = 0;
        EventTime = eventTime;
    }

    public void Update(IntegrationEventPublishStatus publishStatus, int publishTryCount)
    {
        PublishStatus = publishStatus;
        PublishTryCount = publishTryCount;
    }
}
