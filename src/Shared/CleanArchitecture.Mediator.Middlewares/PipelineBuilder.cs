using CleanArchitecture.Mediator.Middlewares.Pipes;

namespace CleanArchitecture.Mediator.Middlewares;

public static class PipelineBuilder
{
    public static IRequestProcessor<TRequest, TResponse> EntryProcessor<TRequest, TResponse>(
        IRequestProcessor<TRequest, TResponse> processor,
        params IMiddleware<TRequest, TResponse>[] filters)
    {
        IRequestProcessor<TRequest, TResponse> result = new LastPipe<TRequest, TResponse>(processor);

        foreach (var filter in filters.Reverse())
        {
            result = new FilterPipe<TRequest, TResponse>(filter, result);
        }

        return result;
    }
}
