using CleanArchitecture.Ordering.Application.Pipelines;
using CleanArchitecture.Ordering.Application.Services;
using Framework.Mediator.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Ordering.Application;

public static class ServiceConfigurations
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddTransient<IQueryService, QueryService>();
        services.AddTransient<ICommandService, CommandService>();
        services.AddTransient(typeof(IBatchCommandsService<>), typeof(BatchCommandsService<>));

        services.AddTransient(typeof(QueryPipeline.Pipeline<,>));
        services.RegisterMiddlewares<QueryPipeline.Configuration>();

        services.AddTransient(typeof(CommandPipeline.Pipeline<,>));
        services.RegisterMiddlewares<CommandPipeline.Configuration>();

        services.AddTransient(typeof(TransactionalCommandHandlingProcessor<,>));
        services.AddTransient(typeof(TransactionalCommandHandler<,>));
    }
}
