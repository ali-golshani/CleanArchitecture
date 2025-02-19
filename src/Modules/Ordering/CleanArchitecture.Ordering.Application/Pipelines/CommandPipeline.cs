using Framework.Mediator.Middlewares;

namespace CleanArchitecture.Ordering.Application.Pipelines;

internal sealed class CommandPipeline<TRequest, TResponse> :
    KeyedPipeline<TRequest, TResponse>
    where TRequest : CommandBase, ICommand<TRequest, TResponse>
{
    public CommandPipeline(
        IServiceProvider serviceProvider,
        TransactionalCommandHandlingProcessor<TRequest, TResponse> processor)
        : base(serviceProvider, processor, CommandPipelineConfiguration.PipelineName)
    { }
}