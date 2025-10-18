using DotNetCore.CAP;
using Framework.Application;
using Microsoft.EntityFrameworkCore;

namespace Framework.Cap;

internal sealed class CapEventOutbox(ICapPublisher publisher) : IIntegrationEventOutbox
{
    private readonly ICapPublisher publisher = publisher;
    private ICapTransaction? publisherTransaction;

    public async Task<IOutboxTransaction> BeginTransaction(DbContext db, CancellationToken cancellationToken)
    {
        var transaction = await db.Database.BeginTransactionAsync(publisher, autoCommit: false, cancellationToken);
        publisherTransaction = publisher.Transaction;
        return new OutboxTransaction(transaction);
    }

    public async Task Publish<TEvent>(
        IReadOnlyCollection<TEvent> events,
        string topic,
        CancellationToken cancellationToken)
    {
        publisher.Transaction = publisherTransaction;

        foreach (var @event in events)
        {
            await publisher.PublishAsync(topic, @event, cancellationToken: cancellationToken);
        }
    }
}