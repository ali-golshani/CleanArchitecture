using CleanArchitecture.Actors;
using CleanArchitecture.Configurations;
using CleanArchitecture.Ordering.Domain.DomainEvents;
using CleanArchitecture.Ordering.Persistence;
using Framework.Persistence.DomainEvents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Ordering.Application.DomainEvents;

internal class OrderEventsPublisher :
    DomainEventsPublisher<OrderDbContext, OrderEventWrapper>
{
    private static readonly Actor Actor = new InternalServiceActor(nameof(OrderEventsPublisher));

    private readonly ILogger logger;

    public OrderEventsPublisher(
        IServiceScopeFactory serviceScopeFactory,
        ILogger<OrderEventsPublisher> logger)
        : base
        (
            serviceScopeFactory: serviceScopeFactory,
            maximumNumberOfRetries: GlobalSettings.DomainEvents.MaximumNumberOfRetries,
            eventWaitingTimeout: GlobalSettings.DomainEvents.EventWaitingTimeout,
            delayOnError: GlobalSettings.DomainEvents.DelayOnError
        )
    {
        this.logger = logger;
    }

    protected override bool IsEnable => true;
    protected override string DistributedLockName => "Ordering.OrderEvents";

    protected async override Task<OrderEventWrapper?> NextEvent(OrderDbContext db)
    {
        var orderEvent = await
            db.Set<OrderEvent>()
            .Where(x => x.IsPublished == null)
            .OrderBy(x => x.EventId)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (orderEvent is null)
        {
            return null;
        }

        return new OrderEventWrapper(orderEvent);
    }

    protected override async Task<OrderEventWrapper?> FindEvent(OrderDbContext db, long eventId)
    {
        var orderEvent = await
            db.Set<OrderEvent>()
            .FirstOrDefaultAsync(x => x.EventId == eventId);

        if (orderEvent is null)
        {
            return null;
        }

        return new OrderEventWrapper(orderEvent);
    }

    protected override Task Log(Exception exception)
    {
        logger.LogError(exception, "{@Publisher} Error", nameof(OrderEventsPublisher));
        return Task.CompletedTask;
    }

    protected override void OnScopeCreated(IServiceScope scope)
    {
        scope.ServiceProvider.ResolveActor(Actor);
    }

    protected override Task PublishEvent(IServiceProvider serviceProvider, OrderEventWrapper @event)
    {
        return PublishEvent(serviceProvider, @event.OrderEvent);
    }

    private static Task PublishEvent(IServiceProvider serviceProvider, OrderEvent orderEvent)
    {
        throw new Framework.Exceptions.NotImplementedException();
    }
}
