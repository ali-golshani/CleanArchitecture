INSERT INTO [cap].[Events]
    ([Id], [EventName], [Topic], [PayloadJson], [OccurredAtUtc],
     [CreatedAtUtc], [CorrelationId], [CausationId])
VALUES
    (@Id, @EventName, @Topic, @PayloadJson, @OccurredAtUtc,
     SYSUTCDATETIME(), @CorrelationId, @CausationId);
