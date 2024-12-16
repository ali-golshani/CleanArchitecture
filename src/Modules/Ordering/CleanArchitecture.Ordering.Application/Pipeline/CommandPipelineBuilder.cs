using CleanArchitecture.Mediator.Middlewares;
using Infrastructure.RequestAudit;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Ordering.Application.Pipeline;

internal sealed class CommandPipelineBuilder<TRequest, TResponse>
    : IPipelineBuilder<TRequest, TResponse>
    where TRequest : CommandBase, ICommand<TRequest, TResponse>
{
    public CommandPipelineBuilder(
        IServiceScopeFactory serviceScopeFactory,
        RequestAuditMiddlewareBuilder auditFilterBuilder,
        ValidationMiddleware<TRequest, TResponse> validation,
        AuthorizationMiddleware<TRequest, TResponse> authorization,
        FilteringMiddleware<TRequest, TResponse> filtering,
        ExceptionHandlingMiddleware<TRequest, TResponse> exceptionHandling)
    {
        var processor = new TransactionalCommandHandlingProcessor<TRequest, TResponse>(serviceScopeFactory);

        var audit = auditFilterBuilder.Build<TRequest, TResponse>(nameof(Ordering));

        var middlewares = new IMiddleware<TRequest, TResponse>[]
        {
            exceptionHandling,
            audit,
            authorization,
            validation,
            filtering
        };

        EntryProcessor = PipelineBuilder.EntryProcessor(middlewares, processor);
    }

    public IRequestProcessor<TRequest, TResponse> EntryProcessor { get; }
}