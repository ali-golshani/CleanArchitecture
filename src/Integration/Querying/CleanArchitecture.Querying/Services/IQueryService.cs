using Framework.Results;

namespace CleanArchitecture.Querying.Services;

public interface IQueryService
{
    Task<Result<TResponse>> Handle<TRequest, TResponse>(IQuery<TRequest, TResponse> query, CancellationToken cancellationToken) where TRequest : QueryBase, IQuery<TRequest, TResponse>;
}