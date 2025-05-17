using CleanArchitecture.Mediator.Middlewares;
using Framework.Mediator.Middlewares;
using Infrastructure.RequestAudit;

namespace CleanArchitecture.Ordering.Application.Pipelines;

internal sealed class CommandPipelineConfiguration : IKeyedPipelineConfiguration
{
    public static string PipelineName { get; } = "OrderingCommandPipeline";

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