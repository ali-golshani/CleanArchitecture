using Framework.Mediator.Middlewares;

namespace CleanArchitecture.Ordering.Queries;

public interface IQueryPipeline<TRequest, TResponse> : IPipeline<TRequest, TResponse>
    where TRequest : QueryBase, IQuery<TRequest, TResponse>
{
}
