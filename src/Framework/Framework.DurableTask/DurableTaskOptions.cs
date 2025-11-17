namespace Framework.DurableTask;

public sealed class DurableTaskOptions
{
    /// <summary>
    /// Gets or sets the number of events that can be dequeued at a time.
    /// </summary>
    public int? WorkItemBatchSize { get; set; }

    /// <summary>
    /// Gets or sets the amount of time a work item is locked after being dequeued.
    /// </summary>
    public TimeSpan? WorkItemLockTimeout { get; set; }

    /// <summary>
    /// Gets or sets the name of the app. Used for logging purposes.
    /// </summary>
    public string? AppName { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of work items that can be processed concurrently by a single worker.
    /// The default value is the value of <see cref="Environment.ProcessorCount"/>.
    /// </summary>
    public int? MaxConcurrentActivities { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of orchestrations that can be loaded in memory at a time by a single worker.
    /// The default value is the value of <see cref="Environment.ProcessorCount"/>.
    /// </summary>
    /// <remarks>
    /// Orchestrations that are idle and waiting for inputs are unloaded from memory and do not count against this limit.
    /// </remarks>
    public int? MaxActiveOrchestrations { get; set; }

    /// <summary>
    /// Gets or sets the minimum interval to poll for orchestrations.
    /// Polling interval increases when no orchestrations or activities are found.
    /// The default value is 50 milliseconds.
    /// </summary>
    public TimeSpan? MinOrchestrationPollingInterval { get; set; }

    /// <summary>
    /// Gets or sets the maximum interval to poll for orchestrations.
    /// Polling interval increases when no orchestrations or activities are found.
    /// The default value is 3 seconds.
    /// </summary>
    public TimeSpan? MaxOrchestrationPollingInterval { get; set; }

    /// <summary>
    /// Gets or sets the delta backoff interval to poll for orchestrations.
    /// Polling interval increases by this delta when no orchestrations are found.
    /// The default value is 50 milliseconds.
    /// </summary>
    public TimeSpan? DeltaBackoffOrchestrationPollingInterval { get; set; }

    /// <summary>
    /// Gets or sets the minimum interval to poll for activities.
    /// Polling interval increases when no activities are found.
    /// The default value is 50 milliseconds.
    /// </summary>
    public TimeSpan? MinActivityPollingInterval { get; set; }

    /// <summary>
    /// Gets or sets the maximum interval to poll for activities.
    /// Polling interval increases when no activities are found.
    /// The default value is 3 seconds.
    /// </summary>
    public TimeSpan? MaxActivityPollingInterval { get; set; }

    /// <summary>
    /// Gets or sets the delta backoff interval to poll for activities.
    /// Polling interval increases by this delta when no activities are found.
    /// The default value is 50 milliseconds.
    /// </summary>
    public TimeSpan? DeltaBackoffActivityPollingInterval { get; set; }
}
