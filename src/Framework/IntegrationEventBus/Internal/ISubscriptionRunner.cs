using IntegrationEventBus.Topology;

namespace IntegrationEventBus.Internal;

internal interface ISubscriptionRunner
{
    Task RunAsync(
        SubscriptionDefinition subscription,
        string processorId,
        CancellationToken cancellationToken);
}
