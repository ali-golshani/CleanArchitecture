;WITH [Active] AS
(
    SELECT
        d.[Id],
        e.[Sequence] AS [EventSequence],
        d.[BlocksFollowing],
        d.[NextAttemptAtUtc]
    FROM [cap].[Deliveries] AS d WITH (UPDLOCK, HOLDLOCK, ROWLOCK)
    INNER JOIN [cap].[Events] AS e ON e.[Id] = d.[EventId]
    WHERE d.[SubscriptionName] = @SubscriptionName
      AND d.[Status] IN (@Pending, @Retrying)
),
[Gate] AS
(
    SELECT MIN([EventSequence]) AS [EventSequence]
    FROM [Active]
    WHERE [BlocksFollowing] = 1
),
[Candidate] AS
(
    SELECT TOP (1) a.[Id]
    FROM [Active] AS a
    CROSS JOIN [Gate] AS g
    WHERE a.[NextAttemptAtUtc] <= @NowUtc
      AND (a.[BlocksFollowing] = 0
           OR g.[EventSequence] IS NULL
           OR a.[EventSequence] <= g.[EventSequence])
    ORDER BY a.[EventSequence]
)
UPDATE d
SET
    d.[Status] = @Retrying,
    d.[AttemptCount] = d.[AttemptCount] + 1,
    d.[LastAttemptAtUtc] = @NowUtc,
    d.[NextAttemptAtUtc] = @NowUtc
OUTPUT
    inserted.[Id],
    e.[Sequence],
    e.[Id],
    e.[EventName],
    e.[Topic],
    e.[PayloadJson],
    e.[OccurredAtUtc],
    e.[CorrelationId],
    e.[CausationId],
    inserted.[AttemptCount],
    inserted.[FirstFailedAtUtc]
FROM [cap].[Deliveries] AS d
INNER JOIN [Candidate] AS c ON c.[Id] = d.[Id]
INNER JOIN [cap].[Events] AS e ON e.[Id] = d.[EventId];
