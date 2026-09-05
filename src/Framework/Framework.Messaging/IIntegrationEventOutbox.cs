using Framework.Mediator.IntegrationEvents;
using System.Data.Common;

namespace Framework.Messaging;

public interface IIntegrationEventOutbox
{
    Task<IOutboxTransaction> BeginTransaction(DbConnection connection, CancellationToken cancellationToken);
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
