namespace Framework.Mediator.Middlewares;

public abstract class Pipeline<TRequest, TResponse> : IPipeline<TRequest, TResponse>
    where TRequest : IRequest<TRequest, TResponse>
{
    protected readonly IRequestProcessor<TRequest, TResponse> entryProcessor;

    protected Pipeline(
        IRequestProcessor<TRequest, TResponse> processor,
        params IMiddleware<TRequest, TResponse>[] middlewares)
    {
        entryProcessor = PipelineBuilder.EntryProcessor(processor, middlewares);
    }

    protected Pipeline(
        IRequestHandler<TRequest, TResponse> handler,
        params IMiddleware<TRequest, TResponse>[] middlewares)
    {
        entryProcessor = PipelineBuilder.EntryProcessor(handler, middlewares);
    }

    public Task<Result<TResponse>> Handle(RequestContext<TRequest> context)
    {
        return entryProcessor.Handle(context);
    }
}
