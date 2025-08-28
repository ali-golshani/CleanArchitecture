using Framework.DependencyInjection.Extensions;
using Framework.Mediator.DomainEvents;
using Microsoft.Extensions.DependencyInjection;
using Minimal.Mediator.Middlewares;
using System.Reflection;

namespace Framework.Mediator.Extensions;

public static class DIExtensions
{
    public static void AddPipeline(this IServiceCollection services, Type pipelineType)
    {
        services.AddTransient(typeof(IPipeline<,>), pipelineType);
    }

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
}
