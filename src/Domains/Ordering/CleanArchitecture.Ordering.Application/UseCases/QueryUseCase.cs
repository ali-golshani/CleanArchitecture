using CleanArchitecture.Actors;
using CleanArchitecture.Authorization;
using CleanArchitecture.Audit;
using CleanArchitecture.Mediator;
using FluentValidation;
using Framework.Exceptions;
using Framework.Results;
using Framework.Validator;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Ordering.Application;

internal sealed class QueryUseCase<TRequest, TResponse> :
    QueryAuditUseCase<TRequest, TResponse>
    where TRequest : QueryBase, IQuery<TRequest, TResponse>
{
    private readonly IActorResolver actorResolver;
    private readonly IRequestHandler<TRequest, TResponse> handler;
    private readonly IValidator<TRequest>[] validators;
    private readonly IAccessVerifier<TRequest>[] accessVerifiers;

    public QueryUseCase(
        IActorResolver actorResolver,
        ILogger<QueryUseCase<TRequest, TResponse>> logger,
        IQueryAuditAgent queryLogger,
        IEnumerable<IValidator<TRequest>> validators,
        IEnumerable<IAccessVerifier<TRequest>> accessVerifiers,
        IRequestHandler<TRequest, TResponse> handler)
        : base(logger, queryLogger)
    {
        this.actorResolver = actorResolver;
        this.handler = handler;
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
            logger.LogError(exp, "{@Query} {@Error}", request, exp);

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

        return await handler.Handle(request, cancellationToken);
    }
}
