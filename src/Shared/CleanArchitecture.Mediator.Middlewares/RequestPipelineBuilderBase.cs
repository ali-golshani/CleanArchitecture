using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Mediator.Middlewares;

public abstract class RequestPipelineBuilderBase<TRequest, TResponse> : IPipelineBuilder<TRequest, TResponse>
{
    protected RequestPipelineBuilderBase(
        IServiceProvider serviceProvider,
        IRequestProcessor<TRequest, TResponse> processor,
        string pipelineName)
    {
        var middlewares =
            serviceProvider
            .GetKeyedServices<IMiddleware<TRequest, TResponse>>(pipelineName)
            .ToArray();

        EntryProcessor = PipelineBuilder.EntryProcessor(processor, middlewares);
    }

    public IRequestProcessor<TRequest, TResponse> EntryProcessor { get; }
}
