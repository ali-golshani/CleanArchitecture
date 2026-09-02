using CleanArchitecture.Actors;
using Framework.Results;
using Framework.Mediator;

namespace CleanArchitecture.Querying.Services;

public interface IQueryService
{
    Task<Result<TResponse>> Handle<TRequest, TResponse>(IQuery<TRequest, TResponse> query, CancellationToken cancellationToken, RequestExecutionOptions? options = null) where TRequest : QueryBase, IQuery<TRequest, TResponse>;
    Task<Result<TResponse>> Handle<TRequest, TResponse>(Actor actor, IQuery<TRequest, TResponse> query, CancellationToken cancellationToken, RequestExecutionOptions? options = null) where TRequest : QueryBase, IQuery<TRequest, TResponse>;
}
