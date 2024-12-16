using CleanArchitecture.Mediator.Middlewares;
using Framework.Mediator.Requests;
using Infrastructure.RequestAudit;

namespace CleanArchitecture.Querying.Services;

internal sealed class QueryPipelineBuilder<TRequest, TResponse>
    : IPipelineBuilder<TRequest, TResponse>
    where TRequest : QueryBase, IQuery<TRequest, TResponse>
{
    public QueryPipelineBuilder(
        IRequestHandler<TRequest, TResponse> handler,
        RequestAuditMiddlewareBuilder auditFilterBuilder,
        ValidationMiddleware<TRequest, TResponse> validation,
        AuthorizationMiddleware<TRequest, TResponse> authorization,
        FilteringMiddleware<TRequest, TResponse> filtering,
        ExceptionHandlingMiddleware<TRequest, TResponse> exceptionHandling)
    {
        var processor = new RequestHandlingProcessor<TRequest, TResponse>(handler);

        var audit = auditFilterBuilder.Build<TRequest, TResponse>(nameof(Querying));

        var middlewares = new IMiddleware<TRequest, TResponse>[]
        {
            exceptionHandling,
            audit,
            authorization,
            validation,
            filtering
        };

        EntryProcessor = PipelineBuilder.EntryProcessor(middlewares, processor);
    }

    public IRequestProcessor<TRequest, TResponse> EntryProcessor { get; }
}
