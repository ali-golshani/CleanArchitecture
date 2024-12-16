namespace CleanArchitecture.Mediator.Middlewares.Pipes;

internal class FilterPipe<TRequest, TResponse> : IRequestProcessor<TRequest, TResponse>
{
    private readonly IMiddleware<TRequest, TResponse> filter;
    private readonly IRequestProcessor<TRequest, TResponse> filterPipe;

    public FilterPipe(IMiddleware<TRequest, TResponse> filter, IRequestProcessor<TRequest, TResponse> filterPipe)
    {
        this.filter = filter;
        this.filterPipe = filterPipe;
    }

    public Task<Result<TResponse>> Handle(RequestContext<TRequest> context)
    {
        return filter.Handle(context, filterPipe);
    }
}
