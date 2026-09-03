using Microsoft.Extensions.Logging;

namespace IntegrationEventBus;

internal static partial class SqlLog
{
    [LoggerMessage(
        EventId = 2000,
        Level = LogLevel.Debug,
        Message = "Processor {ProcessorId} could not acquire the lock for subscription {Subscription}.")]
    public static partial void SubscriptionLockUnavailable(
        ILogger logger,
        string processorId,
        string subscription);

    [LoggerMessage(
        EventId = 2001,
        Level = LogLevel.Information,
        Message = "Processor {ProcessorId} acquired the lock for subscription {Subscription}.")]
    public static partial void SubscriptionLockAcquired(
        ILogger logger,
        string processorId,
        string subscription);

    [LoggerMessage(
        EventId = 2002,
        Level = LogLevel.Error,
        Message = "Subscription {Subscription} processor failed and will reconnect.")]
    public static partial void SubscriptionProcessorFailed(
        ILogger logger,
        Exception exception,
        string subscription);

    [LoggerMessage(
        EventId = 2003,
        Level = LogLevel.Debug,
        Message = "Event {EventId} succeeded for subscription {Subscription} on attempt {Attempt}.")]
    public static partial void DeliverySucceeded(
        ILogger logger,
        Guid eventId,
        string subscription,
        int attempt);

    [LoggerMessage(
        EventId = 2004,
        Level = LogLevel.Error,
        Message = "Event {EventId} was dead-lettered for subscription {Subscription} after {Attempt} attempts.")]
    public static partial void DeliveryDeadLettered(
        ILogger logger,
        Exception exception,
        Guid eventId,
        string subscription,
        int attempt);

    [LoggerMessage(
        EventId = 2005,
        Level = LogLevel.Warning,
        Message = "Event {EventId} failed for subscription {Subscription} on attempt {Attempt}; next attempt is {NextAttemptAtUtc} and blocking is {BlocksFollowingEvents}.")]
    public static partial void DeliveryFailed(
        ILogger logger,
        Exception exception,
        Guid eventId,
        string subscription,
        int attempt,
        DateTimeOffset? nextAttemptAtUtc,
        bool blocksFollowingEvents);
}
