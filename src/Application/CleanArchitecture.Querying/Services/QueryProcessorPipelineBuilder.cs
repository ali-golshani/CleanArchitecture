using CleanArchitecture.Audit;
using CleanArchitecture.Authorization;
using CleanArchitecture.Mediator.Middlewares;
using FluentValidation;
using Framework.Mediator.Requests;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Querying.Services;

internal sealed class QueryProcessorPipelineBuilder<TRequest, TResponse>
    where TRequest : QueryBase, IQuery<TRequest, TResponse>
{
    private const string LoggingDomain = nameof(Querying);

    public QueryProcessorPipelineBuilder(
        IRequestHandler<TRequest, TResponse> handler,
        QueryAuditAgent queryAudit,
        IEnumerable<IValidator<TRequest>> validators,
        IEnumerable<IAccessVerifier<TRequest>> accessVerifiers,
        IEnumerable<IQueryFilter<TRequest>> queryFilters,
        ILogger<QueryUseCase<TRequest, TResponse>> logger)
    {
        var queryHandling = new RequestHandlingProcessor<TRequest, TResponse>(handler);
        var filtering = new QueryFilteringDecorator<TRequest, TResponse>(queryHandling, queryFilters);
        var authorization = new AuthorizationDecorator<TRequest, TResponse>(filtering, accessVerifiers);
        var validation = new ValidationDecorator<TRequest, TResponse>(authorization, validators);
        var audit = new QueryAuditDecorator<TRequest, TResponse>(validation, queryAudit, LoggingDomain, logger);
        var exceptionHandling = new ExceptionHandlingDecorator<TRequest, TResponse>(audit, logger);

        Processor = exceptionHandling;
    }

    public IRequestProcessor<TRequest, TResponse> Processor { get; }
}
