using CleanArchitecture.Authorization;
using CleanArchitecture.Mediator.Middlewares;
using FluentValidation;
using Framework.Mediator.Requests;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Audit;

public abstract class QueryUseCaseBase<TRequest, TResponse>
    where TRequest : Query, IRequest<TRequest, TResponse>
{
    protected abstract string LoggingDomain { get; }

    private readonly IActorResolver actorResolver;
    private readonly ExceptionHandlingDecorator<TRequest, TResponse> handlingUseCase;

    protected QueryUseCaseBase(
        IActorResolver actorResolver,
        IRequestHandler<TRequest, TResponse> handler,
        QueryAuditAgent queryAudit,
        IEnumerable<IValidator<TRequest>> validators,
        IEnumerable<IAccessVerifier<TRequest>> accessVerifiers,
        IEnumerable<IQueryFilter<TRequest>> queryFilters,
        ILogger<QueryUseCaseBase<TRequest, TResponse>> logger)
    {
        this.actorResolver = actorResolver;

        var queryHandling = new QueryHandlingUseCase<TRequest, TResponse>(handler);
        var filtering = new QueryFilteringDecorator<TRequest, TResponse>(queryHandling, queryFilters);
        var authorization = new AuthorizationDecorator<TRequest, TResponse>(filtering, accessVerifiers);
        var validation = new ValidationDecorator<TRequest, TResponse>(authorization, validators);
        var audit = new QueryAuditDecorator<TRequest, TResponse>(validation, queryAudit, LoggingDomain, logger);
        var exceptionHandling = new ExceptionHandlingDecorator<TRequest, TResponse>(audit, logger);

        handlingUseCase = exceptionHandling;
    }

    public async Task<Result<TResponse>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var actor = actorResolver.Actor;

        if (actor is null)
        {
            return ActorNotSpecifiedError.Default;
        }

        return await Handle(actor, request, cancellationToken);
    }

    public async Task<Result<TResponse>> Handle(Actor actor, TRequest request, CancellationToken cancellationToken)
    {
        var context = new UseCaseContext<TRequest>
        {
            Actor = actor,
            Request = request,
            CancellationToken = cancellationToken,
        };

        return await Handle(context);
    }

    public async Task<Result<TResponse>> Handle(UseCaseContext<TRequest> context)
    {
        return await handlingUseCase.Handle(context);
    }
}
