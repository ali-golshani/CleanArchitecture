using CleanArchitecture.Querying.Pipeline;
using Framework.Mediator.Extensions;
using Framework.Results;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Querying.Services;

internal sealed class QueryService(IServiceProvider serviceProvider) : IQueryService
{
    public Task<Result<TResponse>> Handle<TRequest, TResponse>(IQuery<TRequest, TResponse> query, CancellationToken cancellationToken)
        where TRequest : QueryBase, IQuery<TRequest, TResponse>
    {
        var pipeline = serviceProvider.GetRequiredService<QueryPipeline<TRequest, TResponse>>();
        return pipeline.Handle(query.AsRequestType(), cancellationToken);
    }
}
