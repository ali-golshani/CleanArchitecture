using CleanArchitecture.Mediator.Middlewares;
using Framework.Mediator.Middlewares;
using Infrastructure.RequestAudit;

namespace CleanArchitecture.Ordering.Application.Pipelines;

internal static class QueryPipeline
{
    public sealed class Pipeline<TRequest, TResponse> : KeyedPipeline<TRequest, TResponse>
        where TRequest : QueryBase, IQuery<TRequest, TResponse>
    {
        public Pipeline(IServiceProvider serviceProvider)
            : base(serviceProvider, Configuration.PipelineName)
        { }
    }

    public sealed class Configuration : IKeyedPipelineConfiguration
    {
        public static string PipelineName { get; } = "OrderingQueryPipeline";

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
}