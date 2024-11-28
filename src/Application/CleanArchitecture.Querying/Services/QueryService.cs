using Framework.Exceptions;
using Framework.Results;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Querying.Services;

internal sealed class QueryService(IServiceProvider serviceProvider) : IQueryService
{
    public Task<Result<TResponse>> Handle<TRequest, TResponse>(IQuery<TRequest, TResponse> query, CancellationToken cancellationToken)
        where TRequest : QueryBase, IQuery<TRequest, TResponse>
    {
        var pipeline = serviceProvider.GetRequiredService<QueryPipeline<TRequest, TResponse>>();
        if (query is not TRequest request) throw new ProgrammerException();
        return pipeline.Handle(request, cancellationToken);
    }
}
