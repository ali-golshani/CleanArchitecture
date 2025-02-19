using CleanArchitecture.Mediator.Middlewares;
using Framework.Mediator.Middlewares;

namespace CleanArchitecture.Ordering.Application.Pipelines;

internal class QueryPipelineConfiguration : IKeyedPipelineConfiguration
{
    public static string PipelineName => "OrderingQueryPipeline";

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