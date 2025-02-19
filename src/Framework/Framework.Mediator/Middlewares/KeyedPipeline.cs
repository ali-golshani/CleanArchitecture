namespace Framework.Mediator.Middlewares;

public abstract class KeyedPipeline<TRequest, TResponse>
    where TRequest : IRequest<TRequest, TResponse>
{
    protected readonly IRequestProcessor<TRequest, TResponse> processor;

    protected KeyedPipeline(
        IServiceProvider serviceProvider,
        string pipelineName)
    {
        processor = PipelineBuilder.EntryProcessor<TRequest, TResponse>(serviceProvider, pipelineName);
    }

    protected KeyedPipeline(
        IServiceProvider serviceProvider,
        IRequestProcessor<TRequest, TResponse> processor,
        string pipelineName)
    {
        this.processor = PipelineBuilder.EntryProcessor(serviceProvider, processor, pipelineName);
    }

    public Task<Result<TResponse>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var context = new RequestContext<TRequest>
        {
            Request = request,
            CancellationToken = cancellationToken,
        };

        return processor.Handle(context);
    }
}
