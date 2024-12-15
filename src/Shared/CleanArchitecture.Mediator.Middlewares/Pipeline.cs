using CleanArchitecture.Mediator.Middlewares.Pipes;

namespace CleanArchitecture.Mediator.Middlewares;

public sealed class Pipeline<TRequest, TResponse> : IRequestProcessor<TRequest, TResponse>
{
    private readonly IPipe<TRequest, TResponse> entryPipe;

    public Pipeline(
        IFilter<TRequest, TResponse>[] filters,
        IRequestProcessor<TRequest, TResponse> processor)
    {
        entryPipe = new LastPipe<TRequest, TResponse>(processor);

        foreach (var filter in filters.Reverse())
        {
            entryPipe = new FilterPipe<TRequest, TResponse>(filter, entryPipe);
        }
    }

    public Task<Result<TResponse>> Handle(RequestContext<TRequest> context)
    {
        return entryPipe.Send(context);
    }
}
