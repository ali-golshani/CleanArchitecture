using Framework.Threading.BackgroundServices;

namespace CleanArchitecture.Audit;

public abstract class AuditAgent<TLogEntry> : BackgroundServiceAgentBase
{
    protected abstract Task Log(IEnumerable<TLogEntry> logEntries);

    private readonly System.Threading.Channels.Channel<TLogEntry> logChannel;

    protected AuditAgent()
    {
        logChannel = System.Threading.Channels.Channel.CreateUnbounded<TLogEntry>();
    }

    protected override bool IsEnable => true;

    protected virtual TimeSpan[] RetryDelays =>
    [
            TimeSpan.FromSeconds(10),
            TimeSpan.FromSeconds(30),
    ];

    public void Post(TLogEntry logEntry)
    {
        if (ShouldLog(logEntry))
        {
            logChannel.Writer.TryWrite(logEntry);
        }
    }

    protected override IDistributedLock DistributedLock()
    {
        return new EmptyDistributedLock();
    }

    protected virtual bool ShouldLog(TLogEntry logEntry) => true;

    protected override async Task Executing(CancellationToken cancellationToken)
    {
        try
        {
            while (await logChannel.Reader.WaitToReadAsync(cancellationToken))
            {
                while (logChannel.Reader.TryRead(out var value))
                {
                    await TryLogging([value]);
                }
            }
        }
        catch (TaskCanceledException) { DoNothings.Do(); }
        catch (OperationCanceledException) { DoNothings.Do(); }
    }

    private async Task TryLogging(List<TLogEntry> items)
    {
        for (int i = 0; i < RetryDelays.Length + 1; i++)
        {
            try
            {
                await Log(items);
                return;
            }
            catch (Exception)
            {
                if (i < RetryDelays.Length)
                {
                    await Task.Delay(RetryDelays[i]);
                }
            }
        }
    }
}
