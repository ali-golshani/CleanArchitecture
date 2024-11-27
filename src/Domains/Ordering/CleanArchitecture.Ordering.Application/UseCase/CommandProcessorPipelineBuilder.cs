using CleanArchitecture.Audit;
using CleanArchitecture.Authorization;
using CleanArchitecture.Mediator.Middlewares;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Ordering.Application.UseCase;

internal sealed class CommandProcessorPipelineBuilder<TRequest, TResponse>
    where TRequest : CommandBase, ICommand<TRequest, TResponse>
{
    public CommandProcessorPipelineBuilder(
        IServiceScopeFactory serviceScopeFactory,
        CommandAuditAgent commandAudit,
        IEnumerable<IValidator<TRequest>>? validators,
        IEnumerable<IAccessVerifier<TRequest>>? accessVerifiers,
        ILogger<CommandUseCase<TRequest, TResponse>> logger)
    {
        var transactional = new TransactionalCommandHandlingProcessor<TRequest, TResponse>(serviceScopeFactory);
        var authorization = new AuthorizationDecorator<TRequest, TResponse>(transactional, accessVerifiers);
        var validation = new ValidationDecorator<TRequest, TResponse>(authorization, validators);
        var audit = new CommandAuditDecorator<TRequest, TResponse>(validation, commandAudit, nameof(Ordering), logger);
        var exceptionHandling = new ExceptionHandlingDecorator<TRequest, TResponse>(audit, logger);

        Processor = exceptionHandling;
    }

    public IRequestProcessor<TRequest, TResponse> Processor { get; }
}