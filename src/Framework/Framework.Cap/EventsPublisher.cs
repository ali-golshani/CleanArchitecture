using DotNetCore.CAP;
using Framework.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Framework.Cap;

public class EventsPublisher(ICapPublisher publisher)
{
    private readonly ICapPublisher capPublisher = publisher;

    public IDbContextTransaction BeginTransaction(DatabaseContextBase db)
    {
        return db.Database.BeginTransaction(capPublisher, autoCommit: false);
    }

    public async Task PublishEvents<TEvent>(
        DatabaseContextBase db,
        string topic,
        CancellationToken cancellationToken)
        where TEvent : class
    {
        var events = db.TrackingEntries<TEvent>(EntityState.Added).ToList();

        foreach (var @event in events)
        {
            await capPublisher.PublishAsync(topic, @event, cancellationToken: cancellationToken);
        }
    }
}