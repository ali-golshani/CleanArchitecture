namespace Framework.Threading.PeriodicRequestCapturing;

public abstract class PeriodicRequestCapturingStrategy<T> : PeriodicRequestCapturingStrategyBase<T>
{
    private readonly Timer timer;

    protected PeriodicRequestCapturingStrategy(
        int capacity,
        TimeSpan maxDelayTime,
        TimeSpan minIntervalTime,
        TimeSpan? maxIntervalTime = null,
        T? defaultValueOnMaxIntervalEvaluation = default)
        : base(capacity, minIntervalTime, maxIntervalTime, defaultValueOnMaxIntervalEvaluation)
    {
        var dueTime = maxDelayTime;
        timer = new Timer(OnTimer, null, minIntervalTime, dueTime);
    }

    protected override void Disposing()
    {
        timer.Dispose();
    }

    private void OnTimer(object? state) => OnTimer();
}