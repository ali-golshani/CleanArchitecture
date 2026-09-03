UPDATE [cap].[Deliveries]
SET [Status] = @Status,
    [BlocksFollowing] = @BlocksFollowing,
    [NextAttemptAtUtc] = @NextAttemptAtUtc,
    [FirstFailedAtUtc] = COALESCE([FirstFailedAtUtc], @FirstFailedAtUtc),
    [LastError] = @LastError,
    [CompletedAtUtc] = @CompletedAtUtc
WHERE [Id] = @DeliveryId;
