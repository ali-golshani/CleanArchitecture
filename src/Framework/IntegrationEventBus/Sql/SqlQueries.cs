using System.Data;
using IntegrationEventBus.Internal;
using Microsoft.Data.SqlClient;

namespace IntegrationEventBus.Sql;

internal static class SqlQueries
{
    private static readonly string AcquireSubscriptionLockQuery = Properties.Resources.AcquireSubscriptionLock;
    private static readonly string ClaimNextDeliveryQuery = Properties.Resources.ClaimNextDelivery;
    private static readonly string CreateSchemaQuery = Properties.Resources.CreateSchema;
    private static readonly string InsertDeliveryQuery = Properties.Resources.InsertDelivery;
    private static readonly string InsertEventQuery = Properties.Resources.InsertEvent;
    private static readonly string MarkFailedQuery = Properties.Resources.MarkFailed;
    private static readonly string MarkSucceededQuery = Properties.Resources.MarkSucceeded;
    private static readonly string ReleaseCancelledAttemptQuery = Properties.Resources.ReleaseCancelledAttempt;

    public static async Task<int> AcquireSubscriptionLockAsync(
        SqlCommand command,
        string resource,
        CancellationToken cancellationToken)
    {
        command.CommandText = AcquireSubscriptionLockQuery;
        command.Parameters.Add(new SqlParameter("@Resource", SqlDbType.NVarChar, 255) { Value = resource });

        object value = await command.ExecuteScalarAsync(cancellationToken).ConfigureAwait(false);
        return (int)(value ?? throw new InvalidOperationException("sp_getapplock did not return a result."));
    }

    public static async Task<StoredEventDelivery?> ClaimNextDeliveryAsync(
        SqlCommand command,
        string subscriptionName,
        byte pendingStatus,
        byte retryingStatus,
        DateTimeOffset nowUtc,
        CancellationToken cancellationToken)
    {
        command.CommandText = ClaimNextDeliveryQuery;
        command.Parameters.Add(new SqlParameter("@SubscriptionName", SqlDbType.NVarChar, 200) { Value = subscriptionName });
        command.Parameters.Add(new SqlParameter("@Pending", SqlDbType.TinyInt) { Value = pendingStatus });
        command.Parameters.Add(new SqlParameter("@Retrying", SqlDbType.TinyInt) { Value = retryingStatus });
        command.Parameters.Add(new SqlParameter("@NowUtc", SqlDbType.DateTimeOffset) { Value = nowUtc });

        await using var reader = await command.ExecuteReaderAsync(cancellationToken).ConfigureAwait(false);
        if (!await reader.ReadAsync(cancellationToken).ConfigureAwait(false))
        {
            return null;
        }

        return new StoredEventDelivery(
            DeliveryId: reader.GetInt64(0),
            EventSequence: reader.GetInt64(1),
            EventId: reader.GetGuid(2),
            EventName: reader.GetString(3),
            Topic: reader.GetString(4),
            PayloadJson: reader.GetString(5),
            OccurredAtUtc: reader.GetFieldValue<DateTimeOffset>(6),
            CorrelationId: reader.IsDBNull(7) ? null : reader.GetString(7),
            CausationId: reader.IsDBNull(8) ? null : reader.GetGuid(8),
            Attempt: reader.GetInt32(9),
            FirstFailedAtUtc: reader.IsDBNull(10) ? null : reader.GetFieldValue<DateTimeOffset>(10));
    }

    public static async Task CreateSchemaAsync(SqlCommand command, CancellationToken cancellationToken)
    {
        command.CommandText = CreateSchemaQuery;
        await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
    }

    public static async Task InsertEventAsync(
        SqlCommand command,
        Guid id,
        string eventName,
        string topic,
        string payloadJson,
        DateTimeOffset occurredAtUtc,
        string? correlationId,
        Guid? causationId,
        CancellationToken cancellationToken)
    {
        command.CommandText = InsertEventQuery;
        command.Parameters.Add(new SqlParameter("@Id", SqlDbType.UniqueIdentifier) { Value = id });
        command.Parameters.Add(new SqlParameter("@EventName", SqlDbType.NVarChar, 200) { Value = eventName });
        command.Parameters.Add(new SqlParameter("@Topic", SqlDbType.NVarChar, 200) { Value = topic });
        command.Parameters.Add(new SqlParameter("@PayloadJson", SqlDbType.NVarChar, -1) { Value = payloadJson });
        command.Parameters.Add(new SqlParameter("@OccurredAtUtc", SqlDbType.DateTimeOffset) { Value = occurredAtUtc });
        command.Parameters.Add(new SqlParameter("@CorrelationId", SqlDbType.NVarChar, 200) { Value = (object?)correlationId ?? DBNull.Value });
        command.Parameters.Add(new SqlParameter("@CausationId", SqlDbType.UniqueIdentifier) { Value = (object?)causationId ?? DBNull.Value });

        await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
    }

    public static async Task InsertDeliveryAsync(
        SqlCommand command,
        Guid eventId,
        string subscriptionName,
        byte status,
        string retryPolicyName,
        int retryPolicyVersion,
        CancellationToken cancellationToken)
    {
        command.CommandText = InsertDeliveryQuery;
        command.Parameters.Add(new SqlParameter("@EventId", SqlDbType.UniqueIdentifier) { Value = eventId });
        command.Parameters.Add(new SqlParameter("@SubscriptionName", SqlDbType.NVarChar, 200) { Value = subscriptionName });
        command.Parameters.Add(new SqlParameter("@Status", SqlDbType.TinyInt) { Value = status });
        command.Parameters.Add(new SqlParameter("@RetryPolicyName", SqlDbType.NVarChar, 200) { Value = retryPolicyName });
        command.Parameters.Add(new SqlParameter("@RetryPolicyVersion", SqlDbType.Int) { Value = retryPolicyVersion });

        await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
    }

    public static async Task MarkSucceededAsync(
        SqlCommand command,
        long deliveryId,
        byte status,
        DateTimeOffset nowUtc,
        CancellationToken cancellationToken)
    {
        command.CommandText = MarkSucceededQuery;
        command.Parameters.Add(new SqlParameter("@DeliveryId", SqlDbType.BigInt) { Value = deliveryId });
        command.Parameters.Add(new SqlParameter("@Status", SqlDbType.TinyInt) { Value = status });
        command.Parameters.Add(new SqlParameter("@NowUtc", SqlDbType.DateTimeOffset) { Value = nowUtc });

        await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
    }

    public static async Task MarkFailedAsync(
        SqlCommand command,
        long deliveryId,
        byte status,
        bool blocksFollowing,
        DateTimeOffset? nextAttemptAtUtc,
        DateTimeOffset firstFailedAtUtc,
        string error,
        DateTimeOffset? completedAtUtc,
        CancellationToken cancellationToken)
    {
        command.CommandText = MarkFailedQuery;
        command.Parameters.Add(new SqlParameter("@DeliveryId", SqlDbType.BigInt) { Value = deliveryId });
        command.Parameters.Add(new SqlParameter("@Status", SqlDbType.TinyInt) { Value = status });
        command.Parameters.Add(new SqlParameter("@BlocksFollowing", SqlDbType.Bit) { Value = blocksFollowing });
        command.Parameters.Add(new SqlParameter("@NextAttemptAtUtc", SqlDbType.DateTimeOffset) { Value = (object?)nextAttemptAtUtc ?? DBNull.Value });
        command.Parameters.Add(new SqlParameter("@FirstFailedAtUtc", SqlDbType.DateTimeOffset) { Value = firstFailedAtUtc });
        command.Parameters.Add(new SqlParameter("@LastError", SqlDbType.NVarChar, -1) { Value = error });
        command.Parameters.Add(new SqlParameter("@CompletedAtUtc", SqlDbType.DateTimeOffset) { Value = (object?)completedAtUtc ?? DBNull.Value });

        await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
    }

    public static async Task ReleaseCancelledAttemptAsync(
        SqlCommand command,
        long deliveryId,
        DateTimeOffset nowUtc,
        CancellationToken cancellationToken)
    {
        command.CommandText = ReleaseCancelledAttemptQuery;
        command.Parameters.Add(new SqlParameter("@DeliveryId", SqlDbType.BigInt) { Value = deliveryId });
        command.Parameters.Add(new SqlParameter("@NowUtc", SqlDbType.DateTimeOffset) { Value = nowUtc });

        await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
    }
}
