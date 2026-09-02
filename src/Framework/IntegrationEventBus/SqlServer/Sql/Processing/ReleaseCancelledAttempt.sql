UPDATE [cap].[Deliveries]
SET [AttemptCount] = CASE WHEN [AttemptCount] > 0 THEN [AttemptCount] - 1 ELSE 0 END,
    [NextAttemptAtUtc] = @NowUtc
WHERE [Id] = @DeliveryId;
