using Framework.Mediator.Middlewares;

namespace CleanArchitecture.Querying.Pipelines;

internal sealed class QueryPipeline<TRequest, TResponse> :
    KeyedPipeline<TRequest, TResponse>
    where TRequest : QueryBase, IQuery<TRequest, TResponse>
{
    public QueryPipeline(IServiceProvider serviceProvider)
        : base(serviceProvider, QueryPipelineConfiguration.PipelineName)
    { }
}
