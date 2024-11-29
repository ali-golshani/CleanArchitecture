using CleanArchitecture;
using Framework.Threading.BackgroundServices;
using Infrastructure.RequestAudit.Domain;
using Infrastructure.RequestAudit.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.RequestAudit;

public class AuditAgent : BackgroundServiceAgentBase
{
    private readonly IServiceScopeFactory serviceScopeFactory;
    private readonly System.Threading.Channels.Channel<AuditTrail> logChannel;

    public AuditAgent(IServiceScopeFactory serviceScopeFactory)
    {
        this.serviceScopeFactory = serviceScopeFactory;
        logChannel = System.Threading.Channels.Channel.CreateUnbounded<AuditTrail>();
    }

    protected override bool IsEnable => true;

    protected virtual TimeSpan[] RetryDelays =>
    [
            TimeSpan.FromSeconds(10),
            TimeSpan.FromSeconds(30),
    ];

    public void Post(AuditTrail logEntry)
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

    private async Task TryLogging(List<AuditTrail> items)
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

    private static bool ShouldLog(AuditTrail logEntry)
    {
        return
            logEntry.ShouldLog == true ||
            !logEntry.IsSuccess ||
            logEntry.ResponseTime >= GlobalSettings.Audit.QueryResponseTimeThreshold ||
            GlobalSettings.Audit.LogAllRequests
        ;
    }

    private async Task Log(IEnumerable<AuditTrail> logEntries)
    {
        using var scope = serviceScopeFactory.CreateScope();
        using var db = scope.ServiceProvider.GetRequiredService<AuditDbContext>();
        db.Set<AuditTrail>().AddRange(logEntries);
        await db.SaveChangesAsync();
    }
}
