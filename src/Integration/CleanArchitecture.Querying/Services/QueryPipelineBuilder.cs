using CleanArchitecture.Authorization;
using CleanArchitecture.Mediator.Middlewares;
using FluentValidation;
using Framework.Mediator.Requests;
using Microsoft.Extensions.Logging;
using Infrastructure.RequestAudit;

namespace CleanArchitecture.Querying.Services;

internal sealed class QueryPipelineBuilder<TRequest, TResponse>
    : IRequestPipelineBuilder<TRequest, TResponse>
    where TRequest : QueryBase, IQuery<TRequest, TResponse>
{
    private const string LoggingDomain = nameof(Querying);

    public QueryPipelineBuilder(
        IRequestHandler<TRequest, TResponse> handler,
        RequestAuditAgent queryAudit,
        IEnumerable<IValidator<TRequest>> validators,
        IEnumerable<IAccessControl<TRequest>> accessControls,
        IEnumerable<IQueryFilter<TRequest>> queryFilters,
        ILogger<QueryPipelineBuilder<TRequest, TResponse>> logger)
    {
        var queryHandling = new RequestHandlingProcessor<TRequest, TResponse>(handler);
        var queryFiltering = new QueryFilteringDecorator<TRequest, TResponse>(queryHandling, queryFilters);
        var authorization = new AuthorizationDecorator<TRequest, TResponse>(queryFiltering, accessControls);
        var validation = new ValidationDecorator<TRequest, TResponse>(authorization, validators);
        var audit = new RequestAuditDecorator<TRequest, TResponse>(validation, queryAudit, LoggingDomain, logger);
        var exceptionHandling = new ExceptionHandlingDecorator<TRequest, TResponse>(audit, logger);

        EntryProcessor = exceptionHandling;
    }

    public IRequestProcessor<TRequest, TResponse> EntryProcessor { get; }
}
