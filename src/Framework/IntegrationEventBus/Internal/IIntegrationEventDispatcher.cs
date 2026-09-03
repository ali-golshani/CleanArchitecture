using IntegrationEventBus.Topology;

namespace IntegrationEventBus.Internal;

internal interface IIntegrationEventDispatcher
{
    ValueTask DispatchAsync(
        StoredEventDelivery delivery,
        SubscriptionDefinition subscription,
        CancellationToken cancellationToken);
}
