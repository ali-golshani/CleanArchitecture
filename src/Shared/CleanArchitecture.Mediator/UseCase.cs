using CleanArchitecture.Actors;
using Framework.Mediator;
using Framework.Mediator.Middlewares;
using Framework.Results;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Mediator;

public abstract class UseCase<TRequest, TResponse, TPipeline>(ActorPreservingScopeFactory scopeFactory)
    where TRequest : IRequest<TRequest, TResponse>
    where TPipeline : IPipeline<TRequest, TResponse>
{
    public async Task<Result<TResponse>> Execute(TRequest request, CancellationToken cancellationToken)
    {
        using var scope = scopeFactory.CreateScope();
        var pipeline = scope.ServiceProvider.GetRequiredService<TPipeline>();
        return await pipeline.Handle(request, cancellationToken);
    }

    public async Task<Result<TResponse>> Execute(Actor actor, TRequest request, CancellationToken cancellationToken)
    {
        using var scope = scopeFactory.CreateScope(actor);
        var pipeline = scope.ServiceProvider.GetRequiredService<TPipeline>();
        return await pipeline.Handle(request, cancellationToken);
    }
}
