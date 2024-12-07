using CleanArchitecture.Authorization;
using CleanArchitecture.Mediator.Middlewares;
using FluentValidation;
using Framework.Mediator.Requests;
using Microsoft.Extensions.Logging;
using Infrastructure.RequestAudit;

namespace CleanArchitecture.Ordering.Application.Pipeline;

internal sealed class QueryPipelineBuilder<TRequest, TResponse>
    : IRequestPipelineBuilder<TRequest, TResponse>
    where TRequest : QueryBase, IQuery<TRequest, TResponse>
{
    private const string LoggingDomain = nameof(Ordering);

    public QueryPipelineBuilder(
        IRequestHandler<TRequest, TResponse> handler,
        RequestAuditAgent queryAudit,
        IEnumerable<IValidator<TRequest>> validators,
        IEnumerable<IAccessVerifier<TRequest>> accessVerifiers,
        IEnumerable<IQueryFilter<TRequest>> queryFilters,
        IEnumerable<IDataFilter<TResponse>> dataFilters,
        ILogger<QueryPipelineBuilder<TRequest, TResponse>> logger)
    {
        var queryHandling = new RequestHandlingProcessor<TRequest, TResponse>(handler);
        var dataFiltering = new DataFilteringDecorator<TRequest, TResponse>(queryHandling, dataFilters);
        var queryFiltering = new QueryFilteringDecorator<TRequest, TResponse>(dataFiltering, queryFilters);
        var authorization = new AuthorizationDecorator<TRequest, TResponse>(queryFiltering, accessVerifiers);
        var validation = new ValidationDecorator<TRequest, TResponse>(authorization, validators);
        var audit = new RequestAuditDecorator<TRequest, TResponse>(validation, queryAudit, LoggingDomain, logger);
        var exceptionHandling = new ExceptionHandlingDecorator<TRequest, TResponse>(audit, logger);

        EntryProcessor = exceptionHandling;
    }

    public IRequestProcessor<TRequest, TResponse> EntryProcessor { get; }
}
