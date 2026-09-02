using Microsoft.Extensions.Logging;

namespace IntegrationEventBus.Hosting.Logging;

internal static partial class HostingLog
{
    [LoggerMessage(
        EventId = 1000,
        Level = LogLevel.Information,
        Message = "IntegrationEventBus processor has no local subscriptions to run.")]
    public static partial void NoLocalSubscriptions(ILogger logger);

    [LoggerMessage(
        EventId = 1001,
        Level = LogLevel.Information,
        Message = "IntegrationEventBus processor {ProcessorId} is starting {SubscriptionCount} subscription loops.")]
    public static partial void ProcessorStarting(
        ILogger logger,
        string processorId,
        int subscriptionCount);
}
