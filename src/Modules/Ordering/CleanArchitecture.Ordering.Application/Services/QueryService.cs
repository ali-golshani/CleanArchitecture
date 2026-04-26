using CleanArchitecture.Actors;
using CleanArchitecture.Ordering.Application.Pipelines;
using Framework.Mediator.Extensions;
using Framework.Results;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Ordering.Application.Services;

internal sealed class QueryService(ActorPreservingScopeFactory scopeFactory) : IQueryService
{
    public async Task<Result<TResponse>> Handle<TRequest, TResponse>(IQuery<TRequest, TResponse> query, CancellationToken cancellationToken)
        where TRequest : QueryBase, IQuery<TRequest, TResponse>
    {
        using var scope = scopeFactory.CreateScope();
        var pipeline = scope.ServiceProvider.GetRequiredService<QueryPipeline.Pipeline<TRequest, TResponse>>();
        return await pipeline.Handle(query.AsRequestType(), cancellationToken);
    }

    public async Task<Result<TResponse>> Handle<TRequest, TResponse>(Actor actor, IQuery<TRequest, TResponse> query, CancellationToken cancellationToken)
        where TRequest : QueryBase, IQuery<TRequest, TResponse>
    {
        using var scope = scopeFactory.CreateScope(actor);
        var pipeline = scope.ServiceProvider.GetRequiredService<QueryPipeline.Pipeline<TRequest, TResponse>>();
        return await pipeline.Handle(query.AsRequestType(), cancellationToken);
    }
}
