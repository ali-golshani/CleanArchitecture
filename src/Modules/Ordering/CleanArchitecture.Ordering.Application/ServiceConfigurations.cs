using CleanArchitecture.Mediator.Middlewares;
using CleanArchitecture.Mediator.Middlewares.Extensions;
using CleanArchitecture.Ordering.Application.Pipeline;
using CleanArchitecture.Ordering.Application.Services;
using CleanArchitecture.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Ordering.Application;

public static class ServiceConfigurations
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddTransient<IQueryService, QueryService>();
        services.AddTransient<ICommandService, CommandService>();
        services.AddTransient(typeof(IBatchCommandsService<>), typeof(BatchCommandsService<>));

        services.AddTransient(typeof(QueryPipeline<,>));
        services.AddTransient(typeof(CommandPipeline<,>));
        services.AddTransient(typeof(QueryPipelineBuilder<,>));
        services.AddTransient(typeof(CommandPipelineBuilder<,>));

        services.AddTransient(typeof(RequestAuditMiddleware<,>));
        services.AddTransient(typeof(TransactionalCommandHandlingProcessor<,>));

        services.RegisterPipelineMiddlewares(Pipelines.OrderingQuery, QueryMiddlewares());
        services.RegisterPipelineMiddlewares(Pipelines.OrderingCommand, CommandMiddlewares());
    }

    private static Type[] QueryMiddlewares()
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

    private static Type[] CommandMiddlewares()
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
