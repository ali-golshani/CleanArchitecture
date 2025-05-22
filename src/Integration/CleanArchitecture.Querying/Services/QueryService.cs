using CleanArchitecture.Querying.Pipelines;
using Framework.Mediator.Extensions;
using Framework.Results;

namespace CleanArchitecture.Querying.Services;

internal sealed class QueryService(IServiceProvider serviceProvider) : IQueryService
{
    public Task<Result<TResponse>> Handle<TRequest, TResponse>(IQuery<TRequest, TResponse> query, CancellationToken cancellationToken)
        where TRequest : QueryBase, IQuery<TRequest, TResponse>
    {
        return serviceProvider.SendToPipeline<TRequest, TResponse, QueryPipeline.Pipeline<TRequest, TResponse>>(query, cancellationToken);
    }
}
