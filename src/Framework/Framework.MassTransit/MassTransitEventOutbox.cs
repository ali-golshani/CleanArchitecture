using Framework.Application;
using Framework.Persistence;
using Framework.Mediator.IntegrationEvents;
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
        return new MassTransitOutboxTransaction(connection, transaction);
    }

    public async Task Publish(IReadOnlyCollection<IIntegrationEventEnvelope> events, string topic, CancellationToken cancellationToken)
    {
        foreach (var @event in events)
        {
            await publishEndpoint.Publish((object)@event, cancellationToken);
        }

        await massTransitDb.SaveChangesAsync(cancellationToken);
    }
}
