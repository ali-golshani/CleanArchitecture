using CleanArchitecture.Mediator.Middlewares;

namespace CleanArchitecture.Ordering.Application.Pipelines;

internal sealed class CommandPipelineBuilder<TRequest, TResponse>
    : RequestPipelineBuilderBase<TRequest, TResponse>
    where TRequest : CommandBase, ICommand<TRequest, TResponse>
{
    public CommandPipelineBuilder(
        IServiceProvider serviceProvider,
        TransactionalCommandHandlingProcessor<TRequest, TResponse> processor)
        : base(serviceProvider, processor, CommandPipelineConfiguration.PipelineName)
    { }
}
