using Framework.Mediator.Middlewares;

namespace CleanArchitecture.ProcessManager.Pipelines;

internal sealed class RequestPipeline<TRequest, TResponse> :
    KeyedPipeline<TRequest, TResponse>
    where TRequest : RequestBase, IRequest<TRequest, TResponse>
{
    public RequestPipeline(IServiceProvider serviceProvider)
        : base(serviceProvider, RequestPipelineConfiguration.PipelineName)
    { }
}
