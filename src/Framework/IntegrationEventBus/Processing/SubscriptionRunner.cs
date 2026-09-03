using IntegrationEventBus.Internal;
using IntegrationEventBus.Topology;
using IntegrationEventBus.Sql;
using IntegrationEventBus.Storage;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace IntegrationEventBus.Processing;

internal sealed class SubscriptionRunner(
    EventBusOptions options,
    IIntegrationEventDispatcher dispatcher,
    IProcessorSignal processorSignal,
    ILogger<SubscriptionRunner> logger)
    : ISubscriptionRunner
{
    public async Task RunAsync(
        SubscriptionDefinition subscription,
        string processorId,
        CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                await using var connection = new SqlConnection(options.ConnectionString);
                await connection.OpenAsync(cancellationToken).ConfigureAwait(false);

                if (!await TryAcquireLockAsync(connection, subscription, cancellationToken).ConfigureAwait(false))
                {
                    SqlLog.SubscriptionLockUnavailable(logger, processorId, subscription.Name);

                    await processorSignal.WaitAsync(options.LockRetryInterval, cancellationToken).ConfigureAwait(false);
                    continue;
                }

                SqlLog.SubscriptionLockAcquired(logger, processorId, subscription.Name);

                await ProcessUnderLockAsync(connection, subscription, cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception exception)
            {
                SqlLog.SubscriptionProcessorFailed(logger, exception, subscription.Name);

                await processorSignal.WaitAsync(options.LockRetryInterval, cancellationToken).ConfigureAwait(false);
            }
        }
    }

    private async Task ProcessUnderLockAsync(
        SqlConnection connection,
        SubscriptionDefinition subscription,
        CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var delivery = await TryClaimNextAsync(connection, subscription.Name, cancellationToken).ConfigureAwait(false);

            if (delivery is null)
            {
                await processorSignal.WaitAsync(options.PollingInterval, cancellationToken).ConfigureAwait(false);
                continue;
            }

            try
            {
                await dispatcher.DispatchAsync(delivery, subscription, cancellationToken).ConfigureAwait(false);
                await MarkSucceededAsync(connection, delivery.DeliveryId, cancellationToken).ConfigureAwait(false);

                SqlLog.DeliverySucceeded(logger, delivery.EventId, subscription.Name, delivery.Attempt);
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                await ReleaseCancelledAttemptAsync(connection, delivery.DeliveryId, cancellationToken: default).ConfigureAwait(false);
                throw;
            }
            catch (Exception exception)
            {
                var nowUtc = DateTimeOffset.UtcNow;
                var firstFailedAtUtc = delivery.FirstFailedAtUtc ?? nowUtc;

                var decision = RetryPlanner.Plan(
                    subscription.RetryPolicy,
                    delivery.Attempt,
                    firstFailedAtUtc,
                    nowUtc);

                await MarkFailedAsync(
                        connection,
                        delivery.DeliveryId,
                        firstFailedAtUtc,
                        nowUtc,
                        decision,
                        exception,
                        cancellationToken)
                    .ConfigureAwait(false);

                if (decision.IsDeadLetter)
                {
                    SqlLog.DeliveryDeadLettered(
                        logger,
                        exception,
                        delivery.EventId,
                        subscription.Name,
                        delivery.Attempt);
                }
                else
                {
                    SqlLog.DeliveryFailed(
                        logger,
                        exception,
                        delivery.EventId,
                        subscription.Name,
                        delivery.Attempt,
                        decision.NextAttemptAtUtc,
                        decision.BlocksFollowingEvents);
                }
            }
        }
    }

    private async Task<bool> TryAcquireLockAsync(
        SqlConnection connection,
        SubscriptionDefinition subscription,
        CancellationToken cancellationToken)
    {
        var lockInput = $"{SqlConstants.SchemaName}:{subscription.Topic}:{subscription.Name}";
        var lockHash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(lockInput)));
        var resource = $"IntegrationEventBus:{lockHash}";

        await using var command = connection.CreateCommand();
        command.CommandTimeout = options.CommandTimeoutSeconds;
        var result = await SqlQueries.AcquireSubscriptionLockAsync(command, resource, cancellationToken);

        if (result == -1)
        {
            return false;
        }

        if (result < 0)
        {
            throw new InvalidOperationException(
                $"sp_getapplock failed for subscription '{subscription.Name}' with result {result}.");
        }

        return true;
    }

    private async Task<StoredEventDelivery?> TryClaimNextAsync(
        SqlConnection connection,
        string subscriptionName,
        CancellationToken cancellationToken)
    {
        var nowUtc = DateTimeOffset.UtcNow;

        await using var transaction = await connection.BeginTransactionAsync(
            IsolationLevel.ReadCommitted,
            cancellationToken)
            .ConfigureAwait(false);

        await using var command = connection.CreateCommand();
        command.Transaction = (SqlTransaction)transaction;
        command.CommandTimeout = options.CommandTimeoutSeconds;
        var delivery = await SqlQueries.ClaimNextDeliveryAsync(
            command,
            subscriptionName,
            (byte)DeliveryStatus.Pending,
            (byte)DeliveryStatus.Retrying,
            nowUtc,
            cancellationToken);

        await transaction.CommitAsync(cancellationToken).ConfigureAwait(false);
        return delivery;
    }

    private async Task MarkSucceededAsync(
        SqlConnection connection,
        long deliveryId,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandTimeout = options.CommandTimeoutSeconds;
        await SqlQueries.MarkSucceededAsync(
            command,
            deliveryId,
            (byte)DeliveryStatus.Succeeded,
            DateTimeOffset.UtcNow,
            cancellationToken);
    }

    private async Task MarkFailedAsync(
        SqlConnection connection,
        long deliveryId,
        DateTimeOffset firstFailedAtUtc,
        DateTimeOffset nowUtc,
        RetryDecision decision,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var error = exception.ToString();
        if (error.Length > 32_768)
        {
            error = error[..32_768];
        }

        await using var command = connection.CreateCommand();
        command.CommandTimeout = options.CommandTimeoutSeconds;
        await SqlQueries.MarkFailedAsync(
            command,
            deliveryId,
            decision.IsDeadLetter
                ? (byte)DeliveryStatus.DeadLettered
                : (byte)DeliveryStatus.Retrying,
            decision.BlocksFollowingEvents,
            decision.NextAttemptAtUtc,
            firstFailedAtUtc,
            error,
            decision.IsDeadLetter ? nowUtc : null,
            cancellationToken);
    }

    private async Task ReleaseCancelledAttemptAsync(
        SqlConnection connection,
        long deliveryId,
        CancellationToken cancellationToken)
    {
        await using var command = connection.CreateCommand();
        command.CommandTimeout = options.CommandTimeoutSeconds;
        await SqlQueries.ReleaseCancelledAttemptAsync(
            command,
            deliveryId,
            DateTimeOffset.UtcNow,
            cancellationToken);
    }
}
