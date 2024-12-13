using CleanArchitecture.Authorization;
using CleanArchitecture.Mediator.Middlewares;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Infrastructure.RequestAudit;

namespace CleanArchitecture.Ordering.Application.Pipeline;

internal sealed class CommandPipelineBuilder<TRequest, TResponse>
    : IRequestPipelineBuilder<TRequest, TResponse>
    where TRequest : CommandBase, ICommand<TRequest, TResponse>
{
    public CommandPipelineBuilder(
        IServiceScopeFactory serviceScopeFactory,
        RequestAuditAgent commandAudit,
        IEnumerable<IValidator<TRequest>>? validators,
        IEnumerable<IAccessControl<TRequest>>? accessControls,
        ILogger<CommandPipelineBuilder<TRequest, TResponse>> logger)
    {
        var transactional = new TransactionalCommandHandlingProcessor<TRequest, TResponse>(serviceScopeFactory);
        var authorization = new AuthorizationDecorator<TRequest, TResponse>(transactional, accessControls);
        var validation = new ValidationDecorator<TRequest, TResponse>(authorization, validators);
        var audit = new RequestAuditDecorator<TRequest, TResponse>(validation, commandAudit, nameof(Ordering), logger);
        var exceptionHandling = new ExceptionHandlingDecorator<TRequest, TResponse>(audit, logger);

        EntryProcessor = exceptionHandling;
    }

    public IRequestProcessor<TRequest, TResponse> EntryProcessor { get; }
}