using Framework.Mediator.IntegrationEvents;
using Microsoft.EntityFrameworkCore;

namespace Framework.Messaging;

public interface IIntegrationEventOutbox
{
    Task<IOutboxTransaction> BeginTransaction(DbContext db, CancellationToken cancellationToken);
    Task Publish(IReadOnlyCollection<IIntegrationEvent> events, CancellationToken cancellationToken);

    public async Task PublishEvents(IIntegrationEventCollector eventCollector, CancellationToken cancellationToken)
    {
        var events = eventCollector.Drain();

        if (events.Count == 0)
        {
            return;
        }

        await Publish(events, cancellationToken);
    }
}
