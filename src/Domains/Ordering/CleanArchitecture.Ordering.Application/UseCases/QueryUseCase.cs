using CleanArchitecture.Actors;
using CleanArchitecture.Audit;
using CleanArchitecture.Authorization;
using CleanArchitecture.Mediator.Middlewares;
using FluentValidation;
using Framework.Mediator.Requests;
using Framework.Results;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Ordering.Application.UseCases;

internal sealed class QueryUseCase<TRequest, TResponse> :
    UseCaseBase<TRequest, TResponse>,
    IUseCase<TRequest, TResponse>
    where TRequest : QueryBase, IQuery<TRequest, TResponse>
{
    private const string LoggingDomain = nameof(Ordering);

    private readonly ExceptionHandlingDecorator<TRequest, TResponse> handlingUseCase;

    public QueryUseCase(
        IActorResolver actorResolver,
        IRequestHandler<TRequest, TResponse> handler,
        QueryAuditAgent queryAudit,
        IEnumerable<IValidator<TRequest>> validators,
        IEnumerable<IAccessVerifier<TRequest>> accessVerifiers,
        IEnumerable<IQueryFilter<TRequest>> queryFilters,
        ILogger<QueryUseCase<TRequest, TResponse>> logger)
        : base(actorResolver)
    {
        var queryHandling = new RequestHandlingUseCase<TRequest, TResponse>(handler);
        var filtering = new QueryFilteringDecorator<TRequest, TResponse>(queryHandling, queryFilters);
        var authorization = new AuthorizationDecorator<TRequest, TResponse>(filtering, accessVerifiers);
        var validation = new ValidationDecorator<TRequest, TResponse>(authorization, validators);
        var audit = new QueryAuditDecorator<TRequest, TResponse>(validation, queryAudit, LoggingDomain, logger);
        var exceptionHandling = new ExceptionHandlingDecorator<TRequest, TResponse>(audit, logger);

        handlingUseCase = exceptionHandling;
    }

    public override async Task<Result<TResponse>> Handle(UseCaseContext<TRequest> context)
    {
        return await handlingUseCase.Handle(context);
    }
}
