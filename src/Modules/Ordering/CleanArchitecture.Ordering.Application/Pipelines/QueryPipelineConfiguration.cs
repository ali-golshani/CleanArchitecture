using CleanArchitecture.Mediator.Middlewares;

namespace CleanArchitecture.Ordering.Application.Pipelines;

internal static class QueryPipelineConfiguration
{
    public const string PipelineName = "OrderingQueryPipeline";

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