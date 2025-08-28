using CleanArchitecture.Mediator.Middlewares;
using Framework.Results;
using Infrastructure.RequestAudit;
using Minimal.Mediator.Middlewares;

namespace CleanArchitecture.Ordering.Application.Pipelines;

internal static class CommandPipeline
{
    public sealed class Pipeline<TRequest, TResponse> : KeyedPipeline<TRequest, Result<TResponse>>
        where TRequest : CommandBase, ICommand<TRequest, TResponse>
    {
        public Pipeline(IServiceProvider serviceProvider)
            : base(serviceProvider, Configuration.PipelineName)
        { }
    }

    public sealed class Configuration : IKeyedPipelineConfiguration
    {
        public static string PipelineName { get; } = "OrderingCommandPipeline";

        public static MiddlewareDescriptor[] Middlewares()
        {
            return
            [
                typeof(RequestContextMiddleware<,>),
                typeof(ExceptionHandlingMiddleware<,>),
                typeof(RequestAuditMiddleware<,>),
                typeof(AuthorizationMiddleware<,>),
                typeof(ValidationMiddleware<,>),
                typeof(TransactionScopeMiddleware<,>),
                typeof(OrderRequestMiddleware<,>),
            ];
        }
    }
}