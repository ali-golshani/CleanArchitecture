using Framework.Application;
using Framework.Persistence;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Framework.MassTransit;

internal sealed class MassTransitEventOutbox(MassTransitDbContext massTransitDb, IPublishEndpoint publishEndpoint) : IIntegrationEventOutbox
{
    private readonly MassTransitDbContext massTransitDb = massTransitDb;
    private readonly IPublishEndpoint publishEndpoint = publishEndpoint;

    public async Task<IOutboxTransaction> BeginTransaction(DbContext db, CancellationToken cancellationToken)
    {
        (var connection, var transaction) = await DualDbContext.BeginTransaction(db, massTransitDb, cancellationToken);
        return new OutboxTransaction(connection, transaction);
    }

    public async Task Publish<TEvent>(IReadOnlyCollection<TEvent> events, string topic, CancellationToken cancellationToken)
    {
        foreach (var @event in events)
        {
            await publishEndpoint.Publish(@event!, cancellationToken);
        }

        await massTransitDb.SaveChangesAsync(cancellationToken);
    }
}
