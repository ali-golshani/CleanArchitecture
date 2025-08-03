using CleanArchitecture.Actors;
using Framework.Mediator.Middlewares;
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
        var actor = actorResolver.Actor;
        using var scope = serviceScopeFactory.CreateScope(actor);
        var serviceProvider = scope.ServiceProvider;

        var handler = serviceProvider.GetRequiredService<TransactionalCommandHandler<TRequest, TResponse>>();
        return await handler.Handle(context.Request, context.CancellationToken);
    }
}