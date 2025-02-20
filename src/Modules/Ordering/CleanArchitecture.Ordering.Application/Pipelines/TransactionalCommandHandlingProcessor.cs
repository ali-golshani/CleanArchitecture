using CleanArchitecture.Actors;
using CleanArchitecture.Ordering.Application.Utilities;
using CleanArchitecture.Ordering.Persistence;
using Framework.Application;
using Framework.Mediator;
using Framework.Mediator.IntegrationEvents;
using Framework.Mediator.Middlewares;
using Framework.Persistence.Utilities;
using Framework.Results;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Ordering.Application.Pipelines;

internal sealed class TransactionalCommandHandlingProcessor<TRequest, TResponse> :
    IRequestProcessor<TRequest, TResponse>
    where TRequest : CommandBase, ICommand<TRequest, TResponse>
{
    private readonly IActorResolver actorResolver;
    private readonly IServiceScopeFactory serviceScopeFactory;

    public TransactionalCommandHandlingProcessor(
        IActorResolver actorResolver,
        IServiceScopeFactory serviceScopeFactory)
    {
        this.actorResolver = actorResolver;
        this.serviceScopeFactory = serviceScopeFactory;
    }

    public async Task<Result<TResponse>> Handle(RequestContext<TRequest> context)
    {
        var request = context.Request;
        var cancellationToken = context.CancellationToken;

        var actor = actorResolver.Actor;
        using var scope = serviceScopeFactory.CreateScope(actor);
        var serviceProvider = scope.ServiceProvider;

        await using var db = serviceProvider.GetRequiredService<OrderingDbContext>();
        var eventOutbox = serviceProvider.GetRequiredService<IIntegrationEventOutbox>();
        var eventBus = serviceProvider.GetRequiredService<IIntegrationEventBus>();

        using var transaction = await eventOutbox.BeginTransaction(db, cancellationToken);

        var handler = serviceProvider.GetRequiredService<IRequestHandler<TRequest, TResponse>>();
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