using CleanArchitecture.Ordering.Persistence;
using Framework.Application;
using Framework.Application.Extensions;
using Framework.Mediator;
using Framework.Mediator.IntegrationEvents;
using Framework.Persistence.Extensions;
using Framework.Results;

namespace CleanArchitecture.Ordering.Application.Pipelines;

internal sealed class TransactionalCommandHandler<TRequest, TResponse>
    where TRequest : CommandBase, ICommand<TRequest, TResponse>
{
    private readonly OrderingDbContext db;
    private readonly IIntegrationEventOutbox eventOutbox;
    private readonly IIntegrationEventBus eventBus;
    private readonly IRequestHandler<TRequest, TResponse> handler;

    public TransactionalCommandHandler(
        OrderingDbContext db,
        IIntegrationEventOutbox eventOutbox,
        IIntegrationEventBus eventBus,
        IRequestHandler<TRequest, TResponse> handler)
    {
        this.db = db;
        this.eventOutbox = eventOutbox;
        this.eventBus = eventBus;
        this.handler = handler;
    }

    public async Task<Result<TResponse>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        using var transaction = await eventOutbox.BeginTransaction(db, cancellationToken);
        var result = await handler.Handle(request, cancellationToken);

        if (result.IsFailure)
        {
            return result;
        }

        db.LinkCommandCorrelationIds(request.CorrelationId);

        await db.SaveChangesAsync(cancellationToken);
        await eventOutbox.PublishEvents(eventBus, cancellationToken);

        transaction.Commit();

        return result;
    }
}