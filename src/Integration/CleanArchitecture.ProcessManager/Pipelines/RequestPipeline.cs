using CleanArchitecture.Mediator.Middlewares;
using Framework.Results;
using Minimal.Mediator.Middlewares;

namespace CleanArchitecture.ProcessManager.Pipelines;

internal static class RequestPipeline
{
    public sealed class Pipeline<TRequest, TResponse> : KeyedPipeline<TRequest, Result<TResponse>>
        where TRequest : RequestBase, IRequest<TRequest, TResponse>
    {
        public Pipeline(IServiceProvider serviceProvider)
            : base(serviceProvider, Configuration.PipelineName)
        { }
    }

    public sealed class Configuration : IKeyedPipelineConfiguration
    {
        public static string PipelineName { get; } = "ProcessManagerPipeline";

        public static MiddlewareDescriptor[] Middlewares()
        {
            return
            [
                typeof(ExceptionHandlingMiddleware<,>),
                typeof(ValidationMiddleware<,>),
            ];
        }
    }
}