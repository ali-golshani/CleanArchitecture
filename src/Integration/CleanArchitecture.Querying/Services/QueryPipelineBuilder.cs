using CleanArchitecture.Authorization;
using CleanArchitecture.Mediator.Middlewares;
using FluentValidation;
using Framework.Mediator.Requests;
using Microsoft.Extensions.Logging;
using Infrastructure.RequestAudit;

namespace CleanArchitecture.Querying.Services;

internal sealed class QueryPipelineBuilder<TRequest, TResponse>
    : IPipelineBuilder<TRequest, TResponse>
    where TRequest : QueryBase, IQuery<TRequest, TResponse>
{
    private const string LoggingDomain = nameof(Querying);

    public QueryPipelineBuilder(
        IRequestHandler<TRequest, TResponse> handler,
        RequestAuditAgent queryAudit,
        ValidationFilter<TRequest, TResponse> validation,
        AuthorizationFilter<TRequest, TResponse> authorization,
        TransformingFilter<TRequest, TResponse> transforming,
        ExceptionHandlingFilter<TRequest, TResponse> exceptionHandling,
        ILogger<QueryPipelineBuilder<TRequest, TResponse>> logger)
    {
        var queryHandling = new RequestHandlingProcessor<TRequest, TResponse>(handler);
        var audit = new RequestAuditFilter<TRequest, TResponse>(queryAudit, LoggingDomain, logger);

        var filters = new IFilter<TRequest, TResponse>[]
        {
            exceptionHandling,
            audit,
            authorization,
            validation,
            transforming
        };

        EntryProcessor = new Pipeline<TRequest, TResponse>(filters, queryHandling);
    }

    public IRequestProcessor<TRequest, TResponse> EntryProcessor { get; }
}
