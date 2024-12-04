using CleanArchitecture.Actors;
using CleanArchitecture.Mediator.Middlewares;

namespace CleanArchitecture.Ordering.Application.Pipeline;

internal sealed class CommandPipeline<TRequest, TResponse> :
    RequestPipelineBase<TRequest, TResponse>
    where TRequest : CommandBase, ICommand<TRequest, TResponse>
{
    public CommandPipeline(
        IActorResolver actorResolver,
        CommandPipelineBuilder<TRequest, TResponse> pipelineBuilder)
        : base(actorResolver, pipelineBuilder)
    {
    }
}