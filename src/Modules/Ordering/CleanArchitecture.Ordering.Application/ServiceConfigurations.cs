using CleanArchitecture.Ordering.Application.Pipelines;
using CleanArchitecture.Ordering.Application.Services;
using Framework.Mediator.Extensions;
using Framework.Mediator.Middlewares;
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
        services.RegisterMiddlewares<QueryPipelineConfiguration>();
        services.AddTransient(typeof(IPipeline<,>), typeof(QueryPipeline<,>));

        services.AddTransient(typeof(CommandPipeline<,>));
        services.RegisterMiddlewares<CommandPipelineConfiguration>();
        services.AddTransient(typeof(IPipeline<,>), typeof(CommandPipeline<,>));

        services.AddTransient(typeof(TransactionalCommandHandlingProcessor<,>));
    }
}
