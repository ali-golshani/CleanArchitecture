using Framework.Mediator.DomainEvents;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Framework.Mediator.Extensions;

public static class DomainEventsExtensions
{
    private static Assembly[] AllAssemblies() => AppDomain.CurrentDomain.GetAssemblies();

    public static IDomainEventHandler<TEvent> DomainEventHandler<TEvent>(this IServiceProvider serviceProvider)
        where TEvent : IDomainEvent
    {
        return serviceProvider.GetRequiredService<IDomainEventHandler<TEvent>>();
    }

    public static void RegisterAllDomainEventHandlers(this IServiceCollection services)
    {
        var assemblies = AllAssemblies();
        services.RegisterDomainEventHandlers(assemblies);
    }

    public static void RegisterDomainEventHandlers(this IServiceCollection services)
    {
        var assembly = Assembly.GetCallingAssembly();
        services.RegisterDomainEventHandlers(assembly);
    }

    public static void RegisterDomainEventHandlers(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.Scan(scan =>
        {
            scan
                .FromAssemblies(assemblies)
                .AddClasses(classes => classes.AssignableTo(typeof(IDomainEventHandler<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime()
                ;
        });
    }
}
