using CleanArchitecture.Ordering.Persistence;
using Framework.Messaging;
using Framework.Mediator.IntegrationEvents;
using Framework.Mediator.Middlewares;
using Framework.Results;
using Framework.Mediator;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Ordering.Runtime.Pipelines;

internal sealed class TransactionScopeMiddleware<TRequest, TResponse> :
    IMiddleware<TRequest, TResponse>
    where TRequest : CommandBase, ICommand<TRequest, TResponse>
{
    private readonly OrderingDbContext db;
    private readonly IIntegrationEventOutbox eventOutbox;
    private readonly IIntegrationEventCollector eventCollector;

    public TransactionScopeMiddleware(
        OrderingDbContext db,
        IIntegrationEventOutbox eventOutbox,
        IIntegrationEventCollector eventCollector)
    {
        this.db = db;
        this.eventOutbox = eventOutbox;
        this.eventCollector = eventCollector;
    }

    public async Task<Result<TResponse>> Handle(RequestContext<TRequest> context, IRequestProcessor<TRequest, TResponse> next)
    {
        var cancellationToken = context.CancellationToken;

        var connection = db.Database.GetDbConnection();
        await using var transaction = await eventOutbox.BeginTransaction(connection, cancellationToken);
        await using var dbTransaction = await db.Database.UseTransactionAsync(transaction.DbTransaction, cancellationToken);

        var result = await next.Handle(context);

        if (result.IsFailure)
        {
            await transaction.RollbackAsync();
            return result;
        }

        await db.SaveChangesAsync(cancellationToken);
        await eventOutbox.PublishEvents(eventCollector, cancellationToken);

        await transaction.CommitAsync(cancellationToken);

        return result;
    }
}
