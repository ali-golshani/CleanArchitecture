namespace CleanArchitecture.Ordering.Domain.IntegrationEvents;

public abstract class IntegrationEvent
{
    public long EventId { get; }
    public Guid EventGuid { get; }
    public DateTime EventTime { get; }
    public bool? IsPublished { get; private set; }
    public int PublishTryCount { get; private set; }
    public Guid? CommandCorrelationId { get; set; }

    protected IntegrationEvent()
    {
        EventGuid = Guid.NewGuid();
        IsPublished = null;
        PublishTryCount = 0;
        EventTime = DatabaseTime.Now;
    }

    public void Update(bool? isPublished, int publishTryCount)
    {
        IsPublished = isPublished;
        PublishTryCount = publishTryCount;
    }
}
