using CleanArchitecture.Mediator.Middlewares;

namespace CleanArchitecture.Ordering.Application.Pipeline;

internal sealed class CommandPipelineBuilder<TRequest, TResponse>
    : IPipelineBuilder<TRequest, TResponse>
    where TRequest : CommandBase, ICommand<TRequest, TResponse>
{
    public CommandPipelineBuilder(
        TransactionalCommandHandlingProcessor<TRequest, TResponse> processor,
        ExceptionHandlingMiddleware<TRequest, TResponse> exceptionHandling,
        RequestAuditMiddleware<TRequest, TResponse> audit,
        AuthorizationMiddleware<TRequest, TResponse> authorization,
        ValidationMiddleware<TRequest, TResponse> validation,
        FilteringMiddleware<TRequest, TResponse> filtering)
    {
        EntryProcessor = PipelineBuilder.EntryProcessor
        (
            processor,
            exceptionHandling,
            audit,
            authorization,
            validation,
            filtering
        );
    }

    public IRequestProcessor<TRequest, TResponse> EntryProcessor { get; }
}