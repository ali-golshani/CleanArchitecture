using CleanArchitecture.Actors;
using CleanArchitecture.Mediator.Middlewares;

namespace CleanArchitecture.Querying.Pipeline;

internal sealed class QueryPipeline<TRequest, TResponse> :
    RequestPipelineBase<TRequest, TResponse>
    where TRequest : QueryBase, IQuery<TRequest, TResponse>
{
    public QueryPipeline(
        IActorResolver actorResolver,
        QueryPipelineBuilder<TRequest, TResponse> pipelineBuilder)
        : base(actorResolver, pipelineBuilder)
    {
    }
}
