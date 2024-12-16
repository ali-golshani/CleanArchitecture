using CleanArchitecture.Ordering.Application.Services;
using CleanArchitecture.Ordering.Application.Pipeline;
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
    }
}
