using IntegrationEventBus.Core.Topology;

namespace IntegrationEventBus.Core.Infrastructure;

public interface ISubscriptionRunner
{
    Task RunAsync(
        SubscriptionDefinition subscription,
        string processorId,
        CancellationToken cancellationToken);
}
