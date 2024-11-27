using CleanArchitecture.Actors;
using CleanArchitecture.Mediator.Middlewares;
using CleanArchitecture.Ordering.Application.Utilities;
using CleanArchitecture.Ordering.Persistence;
using Framework.Application;
using Framework.Mediator.IntegrationEvents;
using Framework.Mediator.Requests;
using Framework.Persistence.Utilities;
using Framework.Results;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Ordering.Application.RequestProcessors;

internal sealed class TransactionalCommandHandlingProcessor<TRequest, TResponse> :
    IRequestProcessor<TRequest, TResponse>
    where TRequest : CommandBase, ICommand<TRequest, TResponse>
{
    private readonly IServiceScopeFactory serviceScopeFactory;

    public TransactionalCommandHandlingProcessor(IServiceScopeFactory serviceScopeFactory)
    {
        this.serviceScopeFactory = serviceScopeFactory;
    }

    public async Task<Result<TResponse>> Handle(RequestContext<TRequest> context)
    {
        var actor = context.Actor;
        var request = context.Request;
        var cancellationToken = context.CancellationToken;

        using var scope = serviceScopeFactory.CreateScope(actor);

        await using var db = scope.ServiceProvider.GetRequiredService<OrderingDbContext>();
        var eventOutbox = scope.ServiceProvider.GetRequiredService<IIntegrationEventOutbox>();
        var eventBus = scope.ServiceProvider.GetRequiredService<IIntegrationEventBus>();

        using var transaction = await eventOutbox.BeginTransaction(db, cancellationToken);

        var handler = scope.ServiceProvider.GetRequiredService<IRequestHandler<TRequest, TResponse>>();
        var result = await handler.Handle(request, cancellationToken);

        if (result.IsFailure)
        {
            return result;
        }

        CommandCorrelationIdUtility.LinkCommandCorrelationIds(db, request.CorrelationId);

        await db.SaveChangesAsync(cancellationToken);

        await IntegrationEventsPublisher.PublishEvents(eventOutbox, eventBus, cancellationToken);

        transaction.Commit();

        return result;
    }
}