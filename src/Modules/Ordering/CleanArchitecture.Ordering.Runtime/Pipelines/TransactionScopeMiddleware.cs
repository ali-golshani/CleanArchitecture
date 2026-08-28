using CleanArchitecture.Ordering.Persistence;
using Framework.Application;
using Framework.Mediator.IntegrationEvents;
using Framework.Mediator.Middlewares;
using Framework.Results;
using Framework.Mediator;

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

        await using var transaction = await eventOutbox.BeginTransaction(db, cancellationToken);

        var result = await next.Handle(context);

        if (result.IsFailure)
        {
            return result;
        }

        await db.SaveChangesAsync(cancellationToken);
        await eventOutbox.PublishEvents(eventCollector, cancellationToken);

        await transaction.CommitAsync(cancellationToken);

        return result;
    }
}
