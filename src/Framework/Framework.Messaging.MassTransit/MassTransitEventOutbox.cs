using Framework.Messaging;
using Framework.Mediator.IntegrationEvents;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;

namespace Framework.Messaging.MassTransit;

internal sealed class MassTransitEventOutbox(MassTransitDbContext massTransitDb, IPublishEndpoint publishEndpoint) : IIntegrationEventOutbox
{
    private readonly MassTransitDbContext massTransitDb = massTransitDb;
    private readonly IPublishEndpoint publishEndpoint = publishEndpoint;

    public async Task<IOutboxTransaction> BeginTransaction(DbConnection connection, CancellationToken cancellationToken)
    {
        massTransitDb.Database.SetDbConnection(connection, contextOwnsConnection: false);

        if (connection.State != ConnectionState.Open)
        {
            await connection.OpenAsync(cancellationToken);
        }

        var transaction = await connection.BeginTransactionAsync(cancellationToken);
        
        try
        {
            var massTransitTransaction = await massTransitDb.Database.UseTransactionAsync(transaction, cancellationToken);
            return new MassTransitOutboxTransaction(transaction, massTransitTransaction!);
        }
        catch
        {
            await transaction.DisposeAsync();
            throw;
        }
    }

    public async Task Publish(IReadOnlyCollection<IIntegrationEvent> events, CancellationToken cancellationToken)
    {
        foreach (var @event in events)
        {
            await Publish(@event, cancellationToken);
        }

        await massTransitDb.SaveChangesAsync(cancellationToken);
    }

    private async Task Publish(object @event, CancellationToken cancellationToken)
    {
        await publishEndpoint.Publish(@event, cancellationToken);
    }
}
