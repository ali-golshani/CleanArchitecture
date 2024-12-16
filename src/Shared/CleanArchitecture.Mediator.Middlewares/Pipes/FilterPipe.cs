namespace CleanArchitecture.Mediator.Middlewares.Pipes;

internal class FilterPipe<TRequest, TResponse> : IRequestProcessor<TRequest, TResponse>
{
    private readonly IMiddleware<TRequest, TResponse> filter;
    private readonly IRequestProcessor<TRequest, TResponse> pipe;

    public FilterPipe(IMiddleware<TRequest, TResponse> filter, IRequestProcessor<TRequest, TResponse> pipe)
    {
        this.filter = filter;
        this.pipe = pipe;
    }

    public Task<Result<TResponse>> Handle(RequestContext<TRequest> context)
    {
        return filter.Handle(context, pipe);
    }
}
