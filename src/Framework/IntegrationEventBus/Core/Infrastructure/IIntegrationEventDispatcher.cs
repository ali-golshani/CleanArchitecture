using IntegrationEventBus.Core.Topology;

namespace IntegrationEventBus.Core.Infrastructure;

public interface IIntegrationEventDispatcher
{
    ValueTask DispatchAsync(
        StoredEventDelivery delivery,
        SubscriptionDefinition subscription,
        CancellationToken cancellationToken);
}
