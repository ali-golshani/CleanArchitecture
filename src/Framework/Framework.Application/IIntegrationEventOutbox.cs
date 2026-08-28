using Framework.Mediator.IntegrationEvents;
using Microsoft.EntityFrameworkCore;

namespace Framework.Application;

public interface IIntegrationEventOutbox
{
    Task<IOutboxTransaction> BeginTransaction(DbContext db, CancellationToken cancellationToken);
    Task Publish(IReadOnlyCollection<IIntegrationEventEnvelope> events, CancellationToken cancellationToken);

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
