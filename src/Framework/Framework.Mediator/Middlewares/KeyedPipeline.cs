namespace Framework.Mediator.Middlewares;

public abstract class KeyedPipeline<TRequest, TResponse> : IPipeline<TRequest, TResponse>
    where TRequest : IRequest<TRequest, TResponse>
{
    protected readonly IRequestProcessor<TRequest, TResponse> entryProcessor;

    protected KeyedPipeline(IServiceProvider serviceProvider, string pipelineName)
    {
        entryProcessor = PipelineBuilder.EntryProcessor<TRequest, TResponse>(serviceProvider, pipelineName);
    }

    protected KeyedPipeline(
        IServiceProvider serviceProvider,
        IRequestProcessor<TRequest, TResponse> processor,
        string pipelineName)
    {
        entryProcessor = PipelineBuilder.EntryProcessor(serviceProvider, processor, pipelineName);
    }

    public Task<Result<TResponse>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var context = new RequestContext<TRequest>
        {
            Request = request,
            CancellationToken = cancellationToken,
        };

        return entryProcessor.Handle(context);
    }
}
