namespace Framework.Domain.DomainEvents;

public interface IDomainEvent
{
    long EventId { get; }
    public int PublishTryCount { get; }
    public DomainEventPublishStatus PublishStatus { get; }

    void Update(DomainEventPublishStatus publishStatus, int publishTryCount);
}
