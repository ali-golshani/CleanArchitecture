using CleanArchitecture.Actors;
using CleanArchitecture.Authorization;
using CleanArchitecture.Audit;
using CleanArchitecture.Mediator;
using CleanArchitecture.Ordering.Domain.Repositories;
using CleanArchitecture.Ordering.Persistence;
using FluentValidation;
using Framework.Exceptions;
using Framework.Results;
using Framework.Validator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Ordering.Application;

internal sealed class CommandUseCase<TRequest, TResponse> :
    CommandAuditUseCase<TRequest, TResponse>
    where TRequest : CommandBase, ICommand<TRequest, TResponse>
{
    private readonly IActorResolver actorResolver;
    private readonly IValidator<TRequest>[] validators;
    private readonly IAccessVerifier<TRequest>[] accessVerifiers;
    private readonly IServiceScopeFactory serviceScopeFactory;

    public CommandUseCase(
        IActorResolver actorResolver,
        IServiceScopeFactory serviceScopeFactory,
        ILogger<CommandUseCase<TRequest, TResponse>> logger,
        ICommandAuditAgent commandLogger,
        IEnumerable<IValidator<TRequest>>? validators,
        IEnumerable<IAccessVerifier<TRequest>>? accessVerifiers)
        : base(logger, commandLogger)
    {
        this.serviceScopeFactory = serviceScopeFactory;
        this.actorResolver = actorResolver;
        this.validators = validators?.ToArray() ?? [];
        this.accessVerifiers = accessVerifiers?.ToArray() ?? [];
    }

    protected override string LoggingDomain => nameof(Ordering);

    public async Task<Result<TResponse>> Execute(TRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var actor = actorResolver.Actor;

            if (actor is null)
            {
                return ActorNotSpecifiedError.Default;
            }

            return await Execute(request, actor, cancellationToken);
        }
        catch (Exception exp)
        {
            logger.LogError(exp, "{@Command} {@Error}", request, exp);

            var systemException = exp.TranslateToSystemException();
            return new Error(ErrorType.Failure, systemException.Message);
        }
    }

    protected override async Task<Result<TResponse>> InternalExecute(TRequest request, Actor actor, CancellationToken cancellationToken)
    {
        var validationResult = await validators.ValidateAsync(request);
        var errors = validationResult.Errors();

        if (errors.Length > 0)
        {
            return errors;
        }

        if (!await accessVerifiers.IsAccessible(actor, request))
        {
            return AccessDeniedError.Default;
        }

        using var scope = serviceScopeFactory.CreateScope(actor);
        await using var db = scope.ServiceProvider.GetRequiredService<OrderDbContext>();
        var strategy = db.Database.CreateExecutionStrategy();

        var result = await strategy.ExecuteAsync(async delegate
        {
            var handler = RequestHandler(scope.ServiceProvider);
            var result = await handler.Handle(request, cancellationToken);

            if (result.IsFailure)
            {
                return result;
            }

            LinkCommandCorrelationId(db, request);

            await db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return result;
        });

        return result;
    }

    private static IRequestHandler<TRequest, TResponse> RequestHandler(IServiceProvider serviceProvider)
    {
        return serviceProvider.GetRequiredService<IRequestHandler<TRequest, TResponse>>();
    }

    private static void LinkCommandCorrelationId(IOrderDbContext db, TRequest command)
    {
        CommandCorrelationIdUtility.LinkCommandCorrelationId(db, command);
    }
}