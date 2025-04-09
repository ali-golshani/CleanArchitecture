using CleanArchitecture.Mediator.Middlewares;
using Framework.Mediator.Middlewares;
using Infrastructure.RequestAudit;

namespace CleanArchitecture.Querying.Pipelines;

internal sealed class QueryPipelineConfiguration : IKeyedPipelineConfiguration
{
    public static string PipelineName { get; } = "QueryingPipeline";

    public static Type[] Middlewares()
    {
        return
        [
            typeof(ExceptionHandlingMiddleware<,>),
            typeof(RequestAuditMiddleware<,>),
            typeof(AuthorizationMiddleware<,>),
            typeof(ValidationMiddleware<,>),
            typeof(FilteringMiddleware<,>),
        ];
    }
}