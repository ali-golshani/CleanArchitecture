using CleanArchitecture.Actors;
using CleanArchitecture.Mediator.Middlewares;
using Framework.Results;

namespace CleanArchitecture.Ordering.Application.UseCase;

internal sealed class CommandUseCase<TRequest, TResponse> :
    UseCaseBase<TRequest, TResponse>
    where TRequest : CommandBase, ICommand<TRequest, TResponse>
{
    private readonly IRequestProcessor<TRequest, TResponse> processor;

    public CommandUseCase(
        IActorResolver actorResolver,
        CommandProcessorPipelineBuilder<TRequest, TResponse> pipelineBuilder)
        : base(actorResolver)
    {
        processor = pipelineBuilder.Processor;
    }

    public override async Task<Result<TResponse>> Handle(RequestContext<TRequest> context)
    {
        return await processor.Handle(context);
    }
}