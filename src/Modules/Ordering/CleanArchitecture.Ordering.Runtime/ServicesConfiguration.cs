using CleanArchitecture.Ordering.Runtime.Pipelines;
using CleanArchitecture.Ordering.Runtime.Services;
using Framework.Mediator.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Ordering.Runtime;

public static class ServicesConfiguration
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddTransient<IQueryService, QueryService>();
        services.AddKeyedPipeline<QueryPipeline.Configuration>(typeof(QueryPipeline.Pipeline<,>));

        services.AddTransient<ICommandService, CommandService>();
        services.AddKeyedPipeline<CommandPipeline.Configuration>(typeof(CommandPipeline.Pipeline<,>));

        services.AddTransient(typeof(IBatchCommandsService<>), typeof(BatchCommandsService<>));
    }
}
