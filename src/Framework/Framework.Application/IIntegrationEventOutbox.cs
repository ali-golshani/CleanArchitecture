using Framework.Mediator.IntegrationEvents;
using Microsoft.EntityFrameworkCore;

namespace Framework.Application;

public interface IIntegrationEventOutbox
{
    Task<IOutboxTransaction> BeginTransaction(DbContext db, CancellationToken cancellationToken);
    Task Publish<TEvent>(IReadOnlyCollection<TEvent> events, string topic, CancellationToken cancellationToken);

    public async Task PublishEvents(IIntegrationEventBus eventBus, CancellationToken cancellationToken)
    {
        var events = eventBus.Events;

        if (events.Count == 0)
        {
            return;
        }

        var groups = events.GroupBy(x => x.Topic);
        foreach (var group in groups)
        {
            await Publish(group.ToList(), group.Key, cancellationToken);
        }
    }
}