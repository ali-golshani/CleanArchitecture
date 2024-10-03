namespace CleanArchitecture.Ordering.Domain.DomainEvents;

public abstract class DomainEvent
{
    public long EventId { get; }
    public DateTime EventTime { get; }
    public bool? IsPublished { get; private set; }
    public int PublishTryCount { get; private set; }
    public Guid? CommandCorrelationId { get; set; }

    protected DomainEvent()
    {
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
