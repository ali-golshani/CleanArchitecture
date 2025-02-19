namespace Framework.Mediator.Middlewares;

public abstract class Pipeline<TRequest, TResponse>
    : IPipeline<TRequest, TResponse>
    where TRequest : IRequest<TRequest, TResponse>
{
    protected readonly IRequestProcessor<TRequest, TResponse> processor;

    protected Pipeline(
        IRequestProcessor<TRequest, TResponse> processor,
        params IMiddleware<TRequest, TResponse>[] middlewares)
    {
        this.processor = PipelineBuilder.EntryProcessor(processor, middlewares);
    }

    protected Pipeline(
        IRequestHandler<TRequest, TResponse> handler,
        params IMiddleware<TRequest, TResponse>[] middlewares)
    {
        processor = PipelineBuilder.EntryProcessor(handler, middlewares);
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
