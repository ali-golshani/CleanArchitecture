using Framework.Results;

namespace CleanArchitecture.Ordering.Queries;

public interface IQueryService
{
    Task<Result<TResponse>> Handle<TRequest, TResponse>(IQuery<TRequest, TResponse> command, CancellationToken cancellationToken) where TRequest : QueryBase, IQuery<TRequest, TResponse>;
}