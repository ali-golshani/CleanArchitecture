using CleanArchitecture.Actors;
using CleanArchitecture.Audit;
using CleanArchitecture.Authorization;
using CleanArchitecture.Mediator.Middlewares;
using FluentValidation;
using Framework.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Ordering.Application.RequestProcessors;

internal sealed class CommandProcessor<TRequest, TResponse> :
    RequestProcessorBase<TRequest, TResponse>,
    IRequestProcessor<TRequest, TResponse>
    where TRequest : CommandBase, ICommand<TRequest, TResponse>
{
    private readonly ExceptionHandlingDecorator<TRequest, TResponse> handlingProcessor;

    public CommandProcessor(
        IActorResolver actorResolver,
        IServiceScopeFactory serviceScopeFactory,
        CommandAuditAgent commandAudit,
        IEnumerable<IValidator<TRequest>>? validators,
        IEnumerable<IAccessVerifier<TRequest>>? accessVerifiers,
        ILogger<CommandProcessor<TRequest, TResponse>> logger)
        : base(actorResolver)
    {
        var transactional = new TransactionalCommandHandlingProcessor<TRequest, TResponse>(serviceScopeFactory);
        var authorization = new AuthorizationDecorator<TRequest, TResponse>(transactional, accessVerifiers);
        var validation = new ValidationDecorator<TRequest, TResponse>(authorization, validators);
        var audit = new CommandAuditDecorator<TRequest, TResponse>(validation, commandAudit, nameof(Ordering), logger);
        var exceptionHandling = new ExceptionHandlingDecorator<TRequest, TResponse>(audit, logger);

        handlingProcessor = exceptionHandling;
    }

    public override async Task<Result<TResponse>> Handle(RequestContext<TRequest> context)
    {
        return await handlingProcessor.Handle(context);
    }
}