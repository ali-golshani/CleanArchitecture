namespace Framework.Threading.PeriodicRequestCapturing;

public abstract class PeriodicRequestCapturingStrategy : PeriodicRequestCapturingStrategyBase
{
    private readonly Timer timer;

    protected PeriodicRequestCapturingStrategy(
        TimeSpan maxDelayTime,
        TimeSpan minIntervalTime,
        TimeSpan? maxIntervalTime = null)
        : base(minIntervalTime, maxIntervalTime)
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