using CleanArchitecture.Mediator.Middlewares;
using Infrastructure.RequestAudit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Ordering.Application.Pipeline;

internal sealed class CommandPipelineBuilder<TRequest, TResponse>
    : IPipelineBuilder<TRequest, TResponse>
    where TRequest : CommandBase, ICommand<TRequest, TResponse>
{
    public CommandPipelineBuilder(
        IServiceScopeFactory serviceScopeFactory,
        RequestAuditAgent commandAudit,
        ValidationFilter<TRequest, TResponse> validation,
        AuthorizationFilter<TRequest, TResponse> authorization,
        TransformingFilter<TRequest, TResponse> transforming,
        ExceptionHandlingFilter<TRequest, TResponse> exceptionHandling,
        ILogger<CommandPipelineBuilder<TRequest, TResponse>> logger)
    {
        var transactional = new TransactionalCommandHandlingProcessor<TRequest, TResponse>(serviceScopeFactory);
        var audit = new RequestAuditFilter<TRequest, TResponse>(commandAudit, nameof(Ordering), logger);

        var filters = new IFilter<TRequest, TResponse>[]
        {
            exceptionHandling,
            audit,
            authorization,
            validation,
            transforming
        };

        EntryProcessor = new Pipeline<TRequest, TResponse>(filters, transactional);
    }

    public IRequestProcessor<TRequest, TResponse> EntryProcessor { get; }
}