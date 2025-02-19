using CleanArchitecture.Mediator.Middlewares;
using Framework.Mediator.Middlewares;

namespace CleanArchitecture.Querying.Pipelines;

internal class QueryPipelineConfiguration : IKeyedPipelineConfiguration
{
    public static string PipelineName => "QueryingPipeline";

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