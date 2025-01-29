using CleanArchitecture.Mediator.Middlewares;

namespace CleanArchitecture.Ordering.Application.Pipelines;

internal static class CommandPipelineConfiguration
{
    public const string PipelineName = "OrderingCommandPipeline";

    public static Type[] Middlewares()
    {
        return
        [
            typeof(ExceptionHandlingMiddleware<,>),
            typeof(RequestAuditMiddleware<,>),
            typeof(AuthorizationMiddleware<,>),
            typeof(ValidationMiddleware<,>),
            typeof(OrderRequestMiddleware<,>),
        ];
    }
}