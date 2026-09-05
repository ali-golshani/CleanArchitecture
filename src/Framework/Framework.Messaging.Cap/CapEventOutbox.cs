using DotNetCore.CAP;
using Framework.Mediator.IntegrationEvents;
using System.Data.Common;

namespace Framework.Messaging.Cap;

internal sealed class CapEventOutbox(ICapPublisher publisher) : IIntegrationEventOutbox
{
    private readonly ICapPublisher publisher = publisher;

    public Task<IOutboxTransaction> BeginTransaction(DbConnection connection, CancellationToken cancellationToken)
    {
        // Cap do 'sync over async' in BeginTransactionAsync: use sync version here
        var transaction = (DbTransaction)connection.BeginTransaction(publisher, autoCommit: false);

        IOutboxTransaction result = new CapOutboxTransaction(transaction);
        return Task.FromResult(result);
    }

    public async Task Publish(
        IReadOnlyCollection<IIntegrationEvent> events,
        CancellationToken cancellationToken)
    {
        foreach (var @event in events)
        {
            await Publish(@event.Topic, @event, cancellationToken);
        }
    }

    private async Task Publish(string topic, dynamic @event, CancellationToken cancellationToken)
    {
        await publisher.PublishAsync(topic, @event, cancellationToken: cancellationToken);
    }
}
