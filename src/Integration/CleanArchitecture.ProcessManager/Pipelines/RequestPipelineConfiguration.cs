using CleanArchitecture.Mediator.Middlewares;
using Framework.Mediator.Middlewares;

namespace CleanArchitecture.ProcessManager.Pipelines;

internal sealed class RequestPipelineConfiguration : IKeyedPipelineConfiguration
{
    public static string PipelineName { get; } = "ProcessManagerPipeline";

    public static Type[] Middlewares()
    {
        return
        [
            typeof(ExceptionHandlingMiddleware<,>),
            typeof(ValidationMiddleware<,>),
        ];
    }
}