using Framework.Results;
using Minimal.Mediator;

namespace CleanArchitecture.Querying.Services;

internal sealed class QueryService(IMediator mediator) : IQueryService
{
    public Task<Result<TResponse>> Handle<TRequest, TResponse>(IQuery<TRequest, TResponse> query, CancellationToken cancellationToken)
        where TRequest : QueryBase, IQuery<TRequest, TResponse>
    {
        return mediator.Send(query, cancellationToken);
    }
}
