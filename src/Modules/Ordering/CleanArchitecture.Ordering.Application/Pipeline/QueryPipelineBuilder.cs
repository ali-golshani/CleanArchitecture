using CleanArchitecture.Mediator.Middlewares;
using Framework.Mediator.Requests;
using Infrastructure.RequestAudit;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Ordering.Application.Pipeline;

internal sealed class QueryPipelineBuilder<TRequest, TResponse>
    : IPipelineBuilder<TRequest, TResponse>
    where TRequest : QueryBase, IQuery<TRequest, TResponse>
{
    private const string LoggingDomain = nameof(Ordering);

    public QueryPipelineBuilder(
        IRequestHandler<TRequest, TResponse> handler,
        RequestAuditAgent queryAudit,
        ValidationFilter<TRequest, TResponse> validation,
        AuthorizationFilter<TRequest, TResponse> authorization,
        TransformingFilter<TRequest, TResponse> transforming,
        ExceptionHandlingFilter<TRequest, TResponse> exceptionHandling,
        ILogger<QueryPipelineBuilder<TRequest, TResponse>> logger)
    {
        var processor = new RequestHandlingProcessor<TRequest, TResponse>(handler);
        var audit = new RequestAuditFilter<TRequest, TResponse>(queryAudit, LoggingDomain, logger);

        var filters = new IFilter<TRequest, TResponse>[]
        {
            exceptionHandling,
            audit,
            authorization,
            validation,
            transforming
        };

        EntryProcessor = new Pipeline<TRequest, TResponse>(filters, processor);
    }

    public IRequestProcessor<TRequest, TResponse> EntryProcessor { get; }
}
