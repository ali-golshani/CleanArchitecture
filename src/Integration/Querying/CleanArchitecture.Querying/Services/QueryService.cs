using CleanArchitecture.Actors;
using CleanArchitecture.Actors.Extensions;
using CleanArchitecture.Querying.Pipelines;
using Framework.Mediator.Extensions;
using Framework.Results;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Querying.Services;

internal sealed class QueryService(IServiceProvider serviceProvider) : IQueryService
{
    public async Task<Result<TResponse>> Handle<TRequest, TResponse>(IQuery<TRequest, TResponse> query, CancellationToken cancellationToken)
        where TRequest : QueryBase, IQuery<TRequest, TResponse>
    {
        var pipeline = serviceProvider.GetRequiredService<QueryPipeline.Pipeline<TRequest, TResponse>>();
        return await pipeline.Handle(query.AsRequestType(), cancellationToken);
    }

    public async Task<Result<TResponse>> Handle<TRequest, TResponse>(Actor actor, IQuery<TRequest, TResponse> query, CancellationToken cancellationToken)
        where TRequest : QueryBase, IQuery<TRequest, TResponse>
    {
        serviceProvider.ResolveActor(actor);
        var pipeline = serviceProvider.GetRequiredService<QueryPipeline.Pipeline<TRequest, TResponse>>();
        return await pipeline.Handle(query.AsRequestType(), cancellationToken);
    }
}
