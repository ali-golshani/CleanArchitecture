using CleanArchitecture.Mediator.Middlewares;
using Framework.Mediator.Middlewares;
using Infrastructure.RequestAudit;

namespace CleanArchitecture.Ordering.Application.Pipelines;

internal static class CommandPipeline
{
    public sealed class Pipeline<TRequest, TResponse> : KeyedPipeline<TRequest, TResponse>
        where TRequest : CommandBase, ICommand<TRequest, TResponse>
    {
        public Pipeline(
            IServiceProvider serviceProvider,
            TransactionalCommandHandlingProcessor<TRequest, TResponse> processor)
            : base(serviceProvider, processor, Configuration.PipelineName)
        { }
    }

    public sealed class Configuration : IKeyedPipelineConfiguration
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
}