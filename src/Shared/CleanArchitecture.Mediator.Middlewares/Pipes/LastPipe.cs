namespace CleanArchitecture.Mediator.Middlewares.Pipes;

internal class LastPipe<TRequest, TResponse> : IPipe<TRequest, TResponse>
{
    private readonly IRequestProcessor<TRequest, TResponse> processor;

    public LastPipe(IRequestProcessor<TRequest, TResponse> processor)
    {
        this.processor = processor;
    }

    public Task<Result<TResponse>> Send(RequestContext<TRequest> context)
    {
        return processor.Handle(context);
    }
}
