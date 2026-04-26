using Framework.Mediator.Middlewares.Pipes;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Mediator.Middlewares;

internal static class PipelineBuilder
{
    public static IRequestProcessor<TRequest, TResponse> EntryProcessor<TRequest, TResponse>(
        IRequestProcessor<TRequest, TResponse> processor,
        params IMiddleware<TRequest, TResponse>[] middlewares)
    {
        var pipe = processor;

        foreach (var filter in middlewares.Reverse())
        {
            pipe = new FilterPipe<TRequest, TResponse>(filter, pipe);
        }

        return pipe;
    }

    public static IRequestProcessor<TRequest, TResponse> EntryProcessor<TRequest, TResponse>(
        IRequestHandler<TRequest, TResponse> handler,
        params IMiddleware<TRequest, TResponse>[] middlewares)
    where TRequest : IRequest<TRequest, TResponse>
    {
        var processor = new LastPipe<TRequest, TResponse>(handler);
        return EntryProcessor(processor, middlewares);
    }

    internal static IRequestProcessor<TRequest, TResponse> EntryProcessor<TRequest, TResponse>(
        IServiceProvider serviceProvider,
        IRequestProcessor<TRequest, TResponse> processor,
        string pipelineName)
    where TRequest : IRequest<TRequest, TResponse>
    {
        var middlewares =
            serviceProvider
            .GetKeyedServices<IMiddleware<TRequest, TResponse>>(pipelineName)
            .ToArray();

        return EntryProcessor(processor, middlewares);
    }

    internal static IRequestProcessor<TRequest, TResponse> EntryProcessor<TRequest, TResponse>(
        IServiceProvider serviceProvider,
        string pipelineName)
    where TRequest : IRequest<TRequest, TResponse>
    {
        var handler = serviceProvider.GetRequiredService<IRequestHandler<TRequest, TResponse>>();
        var processor = new LastPipe<TRequest, TResponse>(handler);
        return EntryProcessor(serviceProvider, processor, pipelineName);
    }
}
