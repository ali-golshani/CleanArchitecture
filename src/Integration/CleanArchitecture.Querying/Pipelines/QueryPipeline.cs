using CleanArchitecture.Mediator.Middlewares;
using Framework.Results;
using Infrastructure.RequestAudit;
using Minimal.Mediator.Middlewares;

namespace CleanArchitecture.Querying.Pipelines;

internal static class QueryPipeline
{
    public sealed class Pipeline<TRequest, TResponse> : KeyedPipeline<TRequest, Result<TResponse>>
        where TRequest : QueryBase, IQuery<TRequest, TResponse>
    {
        public Pipeline(IServiceProvider serviceProvider)
            : base(serviceProvider, Configuration.PipelineName)
        { }
    }

    public sealed class Configuration : IKeyedPipelineConfiguration
    {
        public static string PipelineName { get; } = "QueryingPipeline";

        public static MiddlewareDescriptor[] Middlewares()
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