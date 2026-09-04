using Framework.Messaging;
using Framework.Mediator.IntegrationEvents;
using IntegrationEventBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;

namespace Framework.Messaging.SqlEventBus;

internal sealed class SqlEventOutbox(IIntegrationEventPublisher publisher) : IIntegrationEventOutbox
{
    private readonly IIntegrationEventPublisher publisher = publisher;
    private DbTransaction? transaction;

    public async Task<IOutboxTransaction> BeginTransaction(DbContext db, CancellationToken cancellationToken)
    {
        var efTransaction = await db.Database.BeginTransactionAsync(cancellationToken);
        transaction = efTransaction.GetDbTransaction();
        return new SqlOutboxTransaction(efTransaction);
    }

    public async Task Publish(
        IReadOnlyCollection<IIntegrationEvent> events,
        CancellationToken cancellationToken)
    {
        foreach (var @event in events)
        {
            await Publish(@event, @event.Header.CorrelationId.ToString(), cancellationToken);
        }
    }

    private async Task Publish(dynamic @event, string correlationId, CancellationToken cancellationToken)
    {
        await publisher.PublishAsync(@event, transaction!, new PublishOptions
        {
            CorrelationId = correlationId
        }, cancellationToken: cancellationToken);
    }
}
