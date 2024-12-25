using CleanArchitecture.Mediator.Middlewares;
using CleanArchitecture.Shared;

namespace CleanArchitecture.Ordering.Application.Pipeline;

internal sealed class CommandPipelineBuilder<TRequest, TResponse>
    : RequestPipelineBuilderBase<TRequest, TResponse>
    where TRequest : CommandBase, ICommand<TRequest, TResponse>
{
    public CommandPipelineBuilder(
        IServiceProvider serviceProvider,
        TransactionalCommandHandlingProcessor<TRequest, TResponse> processor)
        : base(serviceProvider, processor, Pipelines.OrderingCommand)
    { }
}