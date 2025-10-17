namespace Framework.Domain.DomainEvents;

public abstract class DomainEvent
{
    public long EventId { get; }
    public Guid EventGuid { get; }
    public DateTime EventTime { get; }
    public DomainEventPublishStatus PublishStatus { get; private set; }
    public int PublishTryCount { get; private set; }
    public Guid? CorrelationId { get; set; }

    protected DomainEvent(DateTime eventTime)
    {
        EventGuid = Guid.NewGuid();
        PublishStatus = DomainEventPublishStatus.InProcess;
        PublishTryCount = 0;
        EventTime = eventTime;
    }

    public void Update(DomainEventPublishStatus publishStatus, int publishTryCount)
    {
        PublishStatus = publishStatus;
        PublishTryCount = publishTryCount;
    }
}
