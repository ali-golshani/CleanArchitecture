using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanArchitecture.Mediator.DependencyInjection;

public static class InternalEventsExtensions
{
    private static Assembly[] AllAssemblies() => AppDomain.CurrentDomain.GetAssemblies();

    public static IInternalEventHandler<TEvent> InternalEventHandler<TEvent>(this IServiceProvider serviceProvider)
        where TEvent : IInternalEvent
    {
        return serviceProvider.GetRequiredService<IInternalEventHandler<TEvent>>();
    }

    public static void RegisterAllInternalEventHandlers(this IServiceCollection services)
    {
        var assemblies = AllAssemblies();
        RegisterInternalEventHandlers(services, assemblies);
    }

    public static void RegisterInternalEventHandlers(this IServiceCollection services)
    {
        var assembly = Assembly.GetCallingAssembly();
        RegisterInternalEventHandlers(services, assembly);
    }

    public static void RegisterInternalEventHandlers(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.Scan(scan =>
        {
            scan
                .FromAssemblies(assemblies)
                .AddClasses(classes => classes.AssignableTo(typeof(IInternalEventHandler<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime()
                ;
        });
    }
}
