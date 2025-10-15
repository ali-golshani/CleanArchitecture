using Framework.DependencyInjection.Extensions;
using Framework.Mediator.Notifications;
using Framework.Mediator.Middlewares;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Framework.Mediator.Extensions;

public static class DIExtensions
{
    public static void RegisterRequestHandlers(this IServiceCollection services)
    {
        services.RegisterRequestHandlers(Assembly.GetCallingAssembly());
    }

    public static void RegisterRequestHandlers(this IServiceCollection services, Assembly assembly)
    {
        services.RegisterAsImplementedInterfaces(typeof(IRequestHandler<,>), assembly);
    }

    public static void RegisterDomainEventHandlers(this IServiceCollection services)
    {
        services.RegisterDomainEventHandlers(Assembly.GetCallingAssembly());
    }

    public static void RegisterDomainEventHandlers(this IServiceCollection services, Assembly assembly)
    {
        services.RegisterAsImplementedInterfaces(typeof(INotificationHandler<>), assembly);
    }

    public static void AddKeyedPipeline<TPipelineConfiguration>(this IServiceCollection services, Type pipelineType)
        where TPipelineConfiguration : IKeyedPipelineConfiguration
    {
        services.AddTransient(pipelineType);

        foreach (var type in TPipelineConfiguration.Middlewares())
        {
            services.AddKeyedTransient(typeof(IMiddleware<,>), TPipelineConfiguration.PipelineName, type);
        }
    }
}
