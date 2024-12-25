using CleanArchitecture.Mediator.Middlewares;

namespace CleanArchitecture.Ordering.Application.Pipeline;

internal sealed class QueryPipelineBuilder<TRequest, TResponse>
    : RequestPipelineBuilderBase<TRequest, TResponse>
    where TRequest : QueryBase, IQuery<TRequest, TResponse>
{
    public QueryPipelineBuilder(
        IServiceProvider serviceProvider,
        RequestHandlingProcessor<TRequest, TResponse> processor)
        : base(serviceProvider, processor, Pipelines.Query)
    { }
}
