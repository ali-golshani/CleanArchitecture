﻿using CleanArchitecture.Mediator.Middlewares;
using Framework.Mediator.Middlewares;

namespace CleanArchitecture.ProcessManager.Pipelines;

internal static class RequestPipeline
{
    public sealed class Pipeline<TRequest, TResponse> : KeyedPipeline<TRequest, TResponse>
        where TRequest : RequestBase, IRequest<TRequest, TResponse>
    {
        public Pipeline(IServiceProvider serviceProvider)
            : base(serviceProvider, Configuration.PipelineName)
        { }
    }

    public sealed class Configuration : IKeyedPipelineConfiguration
    {
        public static string PipelineName { get; } = "ProcessManagerPipeline";

        public static Type[] Middlewares()
        {
            return
            [
                typeof(ExceptionHandlingMiddleware<,>),
                typeof(ValidationMiddleware<,>),
            ];
        }
    }
}