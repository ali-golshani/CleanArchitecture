using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanArchitecture.Mediator.DependencyInjection;

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
        RegisterDomainEventHandlers(services, assemblies);
    }

    public static void RegisterDomainEventHandlers(this IServiceCollection services)
    {
        var assembly = Assembly.GetCallingAssembly();
        RegisterDomainEventHandlers(services, assembly);
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
