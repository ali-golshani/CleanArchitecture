namespace CleanArchitecture.Mediator.Middlewares.Pipes;

internal class FilterPipe<TRequest, TResponse> : IPipe<TRequest, TResponse>
{
    private readonly IFilter<TRequest, TResponse> filter;
    private readonly IPipe<TRequest, TResponse> filterPipe;

    public FilterPipe(IFilter<TRequest, TResponse> filter, IPipe<TRequest, TResponse> filterPipe)
    {
        this.filter = filter;
        this.filterPipe = filterPipe;
    }

    public Task<Result<TResponse>> Send(RequestContext<TRequest> context)
    {
        return filter.Handle(context, filterPipe);
    }
}
