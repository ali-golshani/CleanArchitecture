using Framework.Domain.IntegrationEvents;
using Framework.Threading.BackgroundServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Persistence.IntegrationEvents;

public abstract class IntegrationEventsPublisher<TDbContext, TEvent>(
    IServiceScopeFactory serviceScopeFactory,
    int maximumNumberOfRetries,
    TimeSpan eventWaitingTimeout,
    TimeSpan delayOnError)
    : BackgroundServiceAgentBase
    where TDbContext : DbContextBase
    where TEvent : class, IIntegrationEvent
{
    protected abstract string DistributedLockName { get; }
    protected abstract Task Log(Exception exception);
    protected abstract void OnScopeCreated(IServiceScope scope);
    protected abstract Task PublishEvent(IServiceProvider serviceProvider, TEvent @event);

    private readonly AutoResetEvent signalEvent = new AutoResetEvent(true);
    private readonly IServiceScopeFactory serviceScopeFactory = serviceScopeFactory;

    protected readonly int maximumNumberOfRetries = maximumNumberOfRetries;
    protected readonly TimeSpan eventWaitingTimeout = eventWaitingTimeout;
    protected readonly TimeSpan delayOnError = delayOnError;

    public void Signal()
    {
        signalEvent.Set();
    }

    protected virtual void WaitingForEvent()
    {
        signalEvent.WaitOne(eventWaitingTimeout);
    }

    protected override IDistributedLock DistributedLock()
    {
        using var scope = CreateScope();
        using var db = scope.ServiceProvider.GetRequiredService<TDbContext>();
        return db.DistributedLock(DistributedLockName);
    }

    protected override Task OnAcquiringDistributedLockFailure()
    {
        return Log(new AcquiringDistributedLockException(DistributedLockName));
    }

    protected override async Task Executing(CancellationToken cancellationToken)
    {
        var wait = true;

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                wait = !await LoadAndEvaluateNextEvent();
            }
            catch (Exception exp)
            {
                await Log(exp);
                await Delay(delayOnError);
            }

            if (wait)
            {
                WaitingForEvent();
            }
        }
    }

    private async Task<bool> LoadAndEvaluateNextEvent()
    {
        var @event = await NextEvent();

        if (@event == null)
        {
            return false;
        }

        var eventId = @event.EventId;
        var tryCount = @event.PublishTryCount + 1;

        try
        {
            await Publish(@event);
            await UpdatePublishStatus(eventId, IntegrationEventPublishStatus.Published, tryCount);
            return true;
        }
        catch
        {
            if (tryCount == maximumNumberOfRetries)
            {
                await UpdatePublishStatus(eventId, IntegrationEventPublishStatus.Failed, tryCount);
            }
            else
            {
                await UpdatePublishStatus(eventId, IntegrationEventPublishStatus.InProcess, tryCount);
            }

            throw;
        }
    }

    private async Task<TEvent?> NextEvent()
    {
        using var scope = CreateScope();
        using var db = scope.ServiceProvider.GetRequiredService<TDbContext>();
        using var transaction = await db.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);
        return await NextEvent(db);
    }

    /// <summary>
    /// return AsNoTracking()
    /// </summary>
    protected async virtual Task<TEvent?> NextEvent(TDbContext db)
    {
        return await
            db.Set<TEvent>()
            .Where(x => x.PublishStatus == IntegrationEventPublishStatus.InProcess)
            .OrderBy(x => x.EventId)
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    /// <summary>
    /// return AsTracking()
    /// </summary>
    protected async virtual Task<TEvent?> FindEvent(TDbContext db, long eventId)
    {
        return await
            db.Set<TEvent>()
            .FirstOrDefaultAsync(x => x.EventId == eventId);
    }

    private async Task Publish(TEvent @event)
    {
        using var scope = CreateScope();
        await PublishEvent(scope.ServiceProvider, @event);
    }

    private async Task UpdatePublishStatus(long eventId, IntegrationEventPublishStatus publishStatus, int publishTryCount)
    {
        using var scope = CreateScope();
        using var db = scope.ServiceProvider.GetRequiredService<TDbContext>();
        using var transaction = await db.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);

        var @event = await FindEvent(db, eventId);

        if (@event is null)
        {
            return;
        }

        @event.Update(publishStatus, publishTryCount);

        await db.SaveChangesAsync();
        await transaction.CommitAsync();
    }

    private IServiceScope CreateScope()
    {
        var scope = serviceScopeFactory.CreateScope();
        OnScopeCreated(scope);
        return scope;
    }
}