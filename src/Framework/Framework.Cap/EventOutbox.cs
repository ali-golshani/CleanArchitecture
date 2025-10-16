using DotNetCore.CAP;
using Framework.Application;
using Microsoft.EntityFrameworkCore;

namespace Framework.Cap;

internal sealed class EventOutbox(ICapPublisher publisher) : IDomainEventOutbox
{
    private readonly ICapPublisher publisher = publisher;

    public async Task<IOutboxTransaction> BeginTransaction(DbContext db, CancellationToken cancellationToken)
    {
        var transaction = await db.Database.BeginTransactionAsync(publisher, autoCommit: false, cancellationToken);
        return new OutboxTransaction(transaction);
    }

    public async Task Publish<TEvent>(
        IReadOnlyCollection<TEvent> events,
        string topic,
        CancellationToken cancellationToken)
    {
        foreach (var @event in events)
        {
            await publisher.PublishAsync(topic, @event, cancellationToken: cancellationToken);
        }
    }
}