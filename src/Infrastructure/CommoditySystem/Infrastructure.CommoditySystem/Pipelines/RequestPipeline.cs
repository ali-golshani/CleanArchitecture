using CleanArchitecture.Actors;
using CleanArchitecture.Mediator.Middlewares;
using Framework.Mediator;

namespace Infrastructure.CommoditySystem.Pipelines;

internal sealed class RequestPipeline<TRequest, TResponse> :
    RequestPipelineBase<TRequest, TResponse>
        where TRequest : RequestBase, IRequest<TRequest, TResponse>
{
    public RequestPipeline(
        IActorResolver actorResolver,
        RequestPipelineBuilder<TRequest, TResponse> pipelineBuilder)
        : base(actorResolver, pipelineBuilder)
    {
    }
}