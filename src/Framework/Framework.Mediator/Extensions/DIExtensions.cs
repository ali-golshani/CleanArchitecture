using Framework.DependencyInjection.Extensions;
using Framework.Mediator.DomainEvents;
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
        services.RegisterAsImplementedInterfaces(typeof(IDomainEventHandler<>), assembly);
    }

    public static void RegisterMiddlewares<TPipelineConfiguration>(this IServiceCollection services)
        where TPipelineConfiguration : IKeyedPipelineConfiguration
    {
        foreach (var type in TPipelineConfiguration.Middlewares())
        {
            services.AddKeyedTransient(typeof(IMiddleware<,>), TPipelineConfiguration.PipelineName, type);
        }
    }
}
