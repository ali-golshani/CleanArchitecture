using CleanArchitecture.Actors;
using CleanArchitecture.Actors.Extensions;
using CleanArchitecture.Ordering.Application.Pipelines;
using Framework.Mediator.Extensions;
using Framework.Results;

namespace CleanArchitecture.Ordering.Application.Services;

internal sealed class QueryService(IServiceProvider serviceProvider) : IQueryService
{
    public Task<Result<TResponse>> Handle<TRequest, TResponse>(IQuery<TRequest, TResponse> query, CancellationToken cancellationToken)
        where TRequest : QueryBase, IQuery<TRequest, TResponse>
    {
        return serviceProvider.SendToPipeline<TRequest, TResponse, QueryPipeline.Pipeline<TRequest, TResponse>>(query, cancellationToken);
    }

    public Task<Result<TResponse>> Handle<TRequest, TResponse>(Actor actor, IQuery<TRequest, TResponse> query, CancellationToken cancellationToken)
        where TRequest : QueryBase, IQuery<TRequest, TResponse>
    {
        serviceProvider.ResolveActor(actor);
        return Handle(query, cancellationToken);
    }
}
