namespace Framework.Cap;

public sealed class CapOptions
{
    public static readonly CapOptions Default = new();

    /// <summary>
    /// The number of message retries, the retry will stop when the threshold is reached.
    /// Default is 50 times.
    /// </summary>
    public int? FailedRetryCount { get; set; }

    /// <summary>
    /// Failed messages polling delay time.
    /// Default is 60 seconds.
    /// </summary>
    public int? FailedRetryInterval { get; set; }

    /// <summary>
    /// The number of consumer thread connections.
    /// Default is 1
    /// </summary>
    public int? ConsumerThreadCount { get; set; }

    /// <summary>
    /// Sent or received succeed message after time span of due, then the message will be deleted at due time.
    /// Default is 24*3600 seconds.
    /// </summary>
    public int? SucceedMessageExpiredAfter { get; set; }

    /// <summary>
    /// Sent or received failed message after time span of due, then the message will be deleted at due time.
    /// Default is 15*24*3600 seconds.
    /// </summary>
    public int? FailedMessageExpiredAfter { get; set; }

    /// <summary>
    /// If true, the message send task will be parallel execute by .net thread pool.
    /// Default is false.
    /// </summary>
    public bool? EnablePublishParallelSend { get; set; }

    /// <summary>
    /// Configure the retry processor to pick up the backtrack time window for Scheduled or Failed status messages.
    /// Default is 240 seconds.
    /// </summary>
    public int? FallbackWindowLookbackSeconds { get; set; }

    /// <summary>
    /// The interval of the collector processor deletes expired messages.
    /// Default is 300 seconds.
    /// </summary>
    public int? CollectorCleaningInterval { get; set; }

    /// <summary>
    /// Maximum number of delayed or queued messages fetched per scheduler cycle.
    /// Default is 1000.
    /// </summary>
    public int? SchedulerBatchSize { get; set; }

    /// <summary>
    /// if true,cap will use only one instance to retry failure messages
    /// </summary>
    public bool? UseStorageLock { get; set; }
}
