using IntegrationEventBus.Internal;
using IntegrationEventBus.Topology;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IntegrationEventBus.Hosting;

internal sealed class IntegrationEventBusHostedService(
    IntegrationEventTopology topology,
    ISubscriptionRunner subscriptionRunner,
    ILogger<IntegrationEventBusHostedService> logger)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var subscriptions =
            topology.Subscriptions
            .Where(static subscription => subscription.HasHandlers)
            .ToArray();

        ValidateHandlers(subscriptions);

        if (subscriptions.Length == 0)
        {
            HostingLog.NoLocalSubscriptions(logger);
            await Task.Delay(Timeout.InfiniteTimeSpan, stoppingToken).ConfigureAwait(false);
            return;
        }

        var processorId = CreateProcessorId();

        HostingLog.ProcessorStarting(logger, processorId, subscriptions.Length);

        var loops =
            subscriptions
            .Select(subscription => subscriptionRunner.RunAsync(subscription, processorId, stoppingToken))
            .ToArray();

        await Task.WhenAll(loops).ConfigureAwait(false);
    }

    private static void ValidateHandlers(IEnumerable<SubscriptionDefinition> subscriptions)
    {
        foreach (var subscription in subscriptions)
        {
            var missingHandlers =
                subscription.Routes
                .Where(static route => route.Value is null)
                .Select(static route => route.Key.FullName ?? route.Key.Name)
                .ToArray();

            if (missingHandlers.Length > 0)
            {
                throw new InvalidOperationException(
                    $"Subscription '{subscription.Name}' is processed in this application but has no " +
                    $"handler for: {string.Join(", ", missingHandlers)}.");
            }
        }
    }

    private static string CreateProcessorId()
    {
        return $"{Environment.MachineName}:{Environment.ProcessId}:{Guid.NewGuid():N}";
    }
}
