using CleanArchitecture.Actors;
using CleanArchitecture.Audit;
using CleanArchitecture.Authorization;
using CleanArchitecture.Mediator.Middlewares;
using FluentValidation;
using Framework.Mediator.Requests;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Querying.Services;

internal sealed class QueryUseCase<TRequest, TResponse> :
    QueryUseCaseBase<TRequest, TResponse>,
    IUseCase<TRequest, TResponse>
    where TRequest : QueryBase, IQuery<TRequest, TResponse>
{
    public QueryUseCase(
        IActorResolver actorResolver,
        IRequestHandler<TRequest, TResponse> handler,
        QueryAuditAgent queryAudit,
        IEnumerable<IValidator<TRequest>> validators,
        IEnumerable<IAccessVerifier<TRequest>> accessVerifiers,
        IEnumerable<IQueryFilter<TRequest>> queryFilters,
        ILogger<QueryUseCase<TRequest, TResponse>> logger)
        : base
        (
            actorResolver: actorResolver,
            handler: handler,
            queryAudit: queryAudit,
            validators: validators,
            accessVerifiers: accessVerifiers,
            queryFilters: queryFilters,
            logger: logger)
    { }

    protected override string LoggingDomain => nameof(Querying);
}
