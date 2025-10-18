using DotNetCore.CAP;
using Framework.Application;
using Microsoft.EntityFrameworkCore;

namespace Framework.Cap;

internal sealed class CapEventOutbox(ICapPublisher publisher) : IIntegrationEventOutbox
{
    private readonly ICapPublisher publisher = publisher;
    private ICapTransaction? publisherTransaction;

    public Task<IOutboxTransaction> BeginTransaction(DbContext db, CancellationToken cancellationToken)
    {
        // Cap do 'sync over async' in BeginTransactionAsync: use sync version here
        var transaction = db.Database.BeginTransaction(publisher, autoCommit: false);
        publisherTransaction = publisher.Transaction;
        IOutboxTransaction result = new CapOutboxTransaction(transaction);
        return Task.FromResult(result);
    }

    public async Task Publish<TEvent>(
        IReadOnlyCollection<TEvent> events,
        string topic,
        CancellationToken cancellationToken)
    {
        publisher.Transaction ??= publisherTransaction;

        foreach (var @event in events)
        {
            await publisher.PublishAsync(topic, @event, cancellationToken: cancellationToken);
        }
    }
}