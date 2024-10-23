using Framework.Persistence.IntegrationEvents;

namespace CleanArchitecture.Ordering.Application.DomainEvents;

internal sealed class OrderEventWrapper : IIntegrationEvent
{
    public OrderEventWrapper(Domain.IntegrationEvents.OrderStatusChangedEvent orderEvent)
    {
        OrderEvent = orderEvent;
    }

    public Domain.IntegrationEvents.OrderStatusChangedEvent OrderEvent { get; }

    public long EventId => OrderEvent.EventId;
    public int PublishTryCount => OrderEvent.PublishTryCount;

    public IntegrationEventPublishStatus PublishStatus
    {
        get
        {
            if (OrderEvent.IsPublished is null)
            {
                return IntegrationEventPublishStatus.InProcess;
            }
            else if (OrderEvent.IsPublished.Value)
            {
                return IntegrationEventPublishStatus.Published;
            }
            else
            {
                return IntegrationEventPublishStatus.Failed;
            }
        }
    }

    public void Update(IntegrationEventPublishStatus publishStatus, int publishTryCount)
    {
        switch (publishStatus)
        {
            case IntegrationEventPublishStatus.Published:
                OrderEvent.Update(true, publishTryCount);
                break;

            case IntegrationEventPublishStatus.Failed:
                OrderEvent.Update(false, publishTryCount);
                break;

            default:
                OrderEvent.Update(null, publishTryCount);
                break;
        }
    }
}
