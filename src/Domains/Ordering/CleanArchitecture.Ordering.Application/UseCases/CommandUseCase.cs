using CleanArchitecture.Actors;
using CleanArchitecture.Audit;
using CleanArchitecture.Authorization;
using CleanArchitecture.Mediator.Middlewares;
using FluentValidation;
using Framework.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Ordering.Application.UseCases;

internal sealed class CommandUseCase<TRequest, TResponse> :
    CommandUseCaseBase<TRequest, TResponse>,
    IUseCase<TRequest, TResponse>
    where TRequest : CommandBase, ICommand<TRequest, TResponse>
{
    private readonly ExceptionHandlingDecorator<TRequest, TResponse> handlingUseCase;

    public CommandUseCase(
        IActorResolver actorResolver,
        IServiceScopeFactory serviceScopeFactory,
        CommandAuditAgent commandAudit,
        IEnumerable<IValidator<TRequest>>? validators,
        IEnumerable<IAccessVerifier<TRequest>>? accessVerifiers,
        ILogger<CommandUseCase<TRequest, TResponse>> logger)
        : base(actorResolver)
    {
        var transactional = new TransactionalCommandHandlingUseCase<TRequest, TResponse>(serviceScopeFactory);
        var authorization = new AuthorizationDecorator<TRequest, TResponse>(transactional, accessVerifiers);
        var validation = new ValidationDecorator<TRequest, TResponse>(authorization, validators);
        var audit = new CommandAuditDecorator<TRequest, TResponse>(validation, commandAudit, nameof(Ordering), logger);
        var exceptionHandling = new ExceptionHandlingDecorator<TRequest, TResponse>(audit, logger);

        handlingUseCase = exceptionHandling;
    }

    public override async Task<Result<TResponse>> Handle(UseCaseContext<TRequest> context)
    {
        return await handlingUseCase.Handle(context);
    }
}