using CleanArchitecture.Mediator.Middlewares.Pipes;

namespace CleanArchitecture.Mediator.Middlewares;

public static class PipelineBuilder
{
    public static IRequestProcessor<TRequest, TResponse> EntryProcessor<TRequest, TResponse>(
        IRequestProcessor<TRequest, TResponse> processor,
        params IMiddleware<TRequest, TResponse>[] middlewares)
    {
        IRequestProcessor<TRequest, TResponse> pipe = new LastPipe<TRequest, TResponse>(processor);

        foreach (var filter in middlewares.Reverse())
        {
            pipe = new FilterPipe<TRequest, TResponse>(filter, pipe);
        }

        return pipe;
    }
}
