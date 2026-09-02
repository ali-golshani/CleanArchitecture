using Framework.Application;
using IntegrationEventBus.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;

namespace Framework.IntegrationEventBus;

internal sealed class EventOutbox(IIntegrationEventPublisher publisher) : IIntegrationEventOutbox
{
    private readonly IIntegrationEventPublisher publisher = publisher;
    private DbTransaction? transaction;

    public async Task<IOutboxTransaction> BeginTransaction(DbContext db, CancellationToken cancellationToken)
    {
        var efTransaction = await db.Database.BeginTransactionAsync(cancellationToken);
        transaction = efTransaction.GetDbTransaction();
        return new OutboxTransaction(transaction);
    }

    public async Task Publish<TEvent>(
        IReadOnlyCollection<TEvent> events,
        string topic,
        CancellationToken cancellationToken)
        where TEvent : notnull
    {
        foreach (var @event in events)
        {
            await publisher.PublishAsync(@event, transaction!, new PublishOptions
            {
                CorrelationId = (@event as Mediator.IntegrationEvents.IIntegrationEvent)?.CorrelationId?.ToString()
            }, cancellationToken: cancellationToken);
        }
    }
}