namespace IntegrationEventBus;

/// <summary>
/// The result of evaluating a failed delivery against its current retry policy.
/// </summary>
public sealed record RetryDecision(
    bool IsDeadLetter,
    bool BlocksFollowingEvents,
    DateTimeOffset? NextAttemptAtUtc);
