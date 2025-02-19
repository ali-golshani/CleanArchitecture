using CleanArchitecture.Mediator.Middlewares;
using Framework.Mediator.Middlewares;

namespace CleanArchitecture.Ordering.Application.Pipelines;

internal class CommandPipelineConfiguration : IKeyedPipelineConfiguration
{
    public static string PipelineName => "OrderingCommandPipeline";

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