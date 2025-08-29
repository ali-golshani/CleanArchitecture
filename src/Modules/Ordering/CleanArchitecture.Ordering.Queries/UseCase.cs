using CleanArchitecture.Actors;
using CleanArchitecture.Mediator;

namespace CleanArchitecture.Ordering.Queries;

public abstract class UseCase<TRequest, TResponse>(ActorPreservingScopeFactory scopeFactory)
    : UseCase<TRequest, TResponse, IQueryPipeline<TRequest, TResponse>>(scopeFactory)
    where TRequest : QueryBase, IQuery<TRequest, TResponse>;