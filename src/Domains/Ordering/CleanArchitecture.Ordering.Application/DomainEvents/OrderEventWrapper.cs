using Framework.Persistence.DomainEvents;

namespace CleanArchitecture.Ordering.Application.DomainEvents;

internal sealed class OrderEventWrapper : IDomainEvent
{
    public OrderEventWrapper(Domain.DomainEvents.OrderEvent orderEvent)
    {
        OrderEvent = orderEvent;
    }

    public Domain.DomainEvents.OrderEvent OrderEvent { get; }

    public long EventId => OrderEvent.EventId;
    public int PublishTryCount => OrderEvent.PublishTryCount;

    public DomainEventPublishStatus PublishStatus
    {
        get
        {
            if (OrderEvent.IsPublished is null)
            {
                return DomainEventPublishStatus.InProcess;
            }
            else if (OrderEvent.IsPublished.Value)
            {
                return DomainEventPublishStatus.Published;
            }
            else
            {
                return DomainEventPublishStatus.Failed;
            }
        }
    }

    public void Update(DomainEventPublishStatus publishStatus, int publishTryCount)
    {
        switch (publishStatus)
        {
            case DomainEventPublishStatus.Published:
                OrderEvent.Update(true, publishTryCount);
                break;

            case DomainEventPublishStatus.Failed:
                OrderEvent.Update(false, publishTryCount);
                break;

            default:
                OrderEvent.Update(null, publishTryCount);
                break;
        }
    }
}
