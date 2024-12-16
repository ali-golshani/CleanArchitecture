using CleanArchitecture.Mediator.Middlewares;

namespace CleanArchitecture.Ordering.Application.Pipeline;

internal sealed class QueryPipelineBuilder<TRequest, TResponse>
    : IPipelineBuilder<TRequest, TResponse>
    where TRequest : QueryBase, IQuery<TRequest, TResponse>
{
    public QueryPipelineBuilder(
        RequestHandlingProcessor<TRequest, TResponse> processor,
        ExceptionHandlingMiddleware<TRequest, TResponse> exceptionHandling,
        RequestAuditMiddleware<TRequest, TResponse> audit,
        AuthorizationMiddleware<TRequest, TResponse> authorization,
        ValidationMiddleware<TRequest, TResponse> validation,
        FilteringMiddleware<TRequest, TResponse> filtering)
    {
        EntryProcessor = PipelineBuilder.EntryProcessor
        (
            processor,
            exceptionHandling,
            audit,
            authorization,
            validation,
            filtering
        );
    }

    public IRequestProcessor<TRequest, TResponse> EntryProcessor { get; }
}
