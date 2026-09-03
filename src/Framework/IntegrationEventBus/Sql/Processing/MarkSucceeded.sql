UPDATE [cap].[Deliveries]
SET [Status] = @Status,
    [BlocksFollowing] = 0,
    [NextAttemptAtUtc] = NULL,
    [CompletedAtUtc] = @NowUtc,
    [LastError] = NULL
WHERE [Id] = @DeliveryId;
