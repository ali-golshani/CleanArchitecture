using Framework.Mediator.IntegrationEvents;
using Microsoft.EntityFrameworkCore;

namespace Framework.Application;

public interface IIntegrationEventOutbox
{
    Task<IOutboxTransaction> BeginTransaction(DbContext db, CancellationToken cancellationToken);
    Task Publish(IReadOnlyCollection<IIntegrationEventEnvelope> events, string topic, CancellationToken cancellationToken);

    public async Task PublishEvents(IIntegrationEventCollector eventCollector, CancellationToken cancellationToken)
    {
        var events = eventCollector.Drain();

        if (events.Count == 0)
        {
            return;
        }

        var groups = events.GroupBy(x => x.Topic);
        foreach (var group in groups)
        {
            await Publish([.. group], group.Key, cancellationToken);
        }
    }
}
