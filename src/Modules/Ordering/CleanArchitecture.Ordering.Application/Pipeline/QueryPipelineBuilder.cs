using CleanArchitecture.Mediator.Middlewares;
using Framework.Mediator.Requests;
using Infrastructure.RequestAudit;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Ordering.Application.Pipeline;

internal sealed class QueryPipelineBuilder<TRequest, TResponse>
    : IPipelineBuilder<TRequest, TResponse>
    where TRequest : QueryBase, IQuery<TRequest, TResponse>
{
    public QueryPipelineBuilder(
        IRequestHandler<TRequest, TResponse> handler,
        RequestAuditMiddlewareBuilder auditFilterBuilder,
        ValidationMiddleware<TRequest, TResponse> validation,
        AuthorizationMiddleware<TRequest, TResponse> authorization,
        TransformingMiddleware<TRequest, TResponse> transforming,
        ExceptionHandlingMiddleware<TRequest, TResponse> exceptionHandling)
    {
        var processor = new RequestHandlingProcessor<TRequest, TResponse>(handler);

        var audit = auditFilterBuilder.Build<TRequest, TResponse>(nameof(Ordering));

        var middlewares = new IMiddleware<TRequest, TResponse>[]
        {
            exceptionHandling,
            audit,
            authorization,
            validation,
            transforming
        };

        EntryProcessor = PipelineBuilder.EntryProcessor(middlewares, processor);
    }

    public IRequestProcessor<TRequest, TResponse> EntryProcessor { get; }
}
