using CleanArchitecture.Ordering.Application.Services;
using CleanArchitecture.Ordering.Application.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using CleanArchitecture.Mediator.Middlewares;

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

        RegisterQueryMiddlewares(services);
        RegisterCommandMiddlewares(services);
    }

    private static void RegisterQueryMiddlewares(IServiceCollection services)
    {
        var name = Pipelines.Query;

        services.AddKeyedTransient(typeof(IMiddleware<,>), name, typeof(ExceptionHandlingMiddleware<,>));
        services.AddKeyedTransient(typeof(IMiddleware<,>), name, typeof(RequestAuditMiddleware<,>));
        services.AddKeyedTransient(typeof(IMiddleware<,>), name, typeof(AuthorizationMiddleware<,>));
        services.AddKeyedTransient(typeof(IMiddleware<,>), name, typeof(ValidationMiddleware<,>));
        services.AddKeyedTransient(typeof(IMiddleware<,>), name, typeof(FilteringMiddleware<,>));
    }

    private static void RegisterCommandMiddlewares(IServiceCollection services)
    {
        var name = Pipelines.Command;

        services.AddKeyedTransient(typeof(IMiddleware<,>), name, typeof(ExceptionHandlingMiddleware<,>));
        services.AddKeyedTransient(typeof(IMiddleware<,>), name, typeof(RequestAuditMiddleware<,>));
        services.AddKeyedTransient(typeof(IMiddleware<,>), name, typeof(AuthorizationMiddleware<,>));
        services.AddKeyedTransient(typeof(IMiddleware<,>), name, typeof(ValidationMiddleware<,>));
        services.AddKeyedTransient(typeof(IMiddleware<,>), name, typeof(OrderRequestMiddleware<,>));
    }
}
