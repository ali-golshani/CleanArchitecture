using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Framework.DependencyInjection.Extensions;

public static class DIExtensions
{
    /// <summary>
    /// Register All types derived from 'TBase' as Self with Transient-Lifetime
    /// </summary>
    public static void RegisterAsSelf<TBase>(
        this IServiceCollection services,
        Assembly assembly)
    {
        services.Scan(scan =>
        {
            scan
                .FromAssemblies(assembly)
                .AddClasses(classes => classes.AssignableTo<TBase>())
                .AsSelf()
                .WithTransientLifetime()
                ;
        });
    }

    /// <summary>
    /// Register All types derived from 'baseType' as Self with Transient-Lifetime
    /// </summary>
    public static void RegisterAsSelf(
        this IServiceCollection services,
        Type baseType,
        Assembly assembly)
    {
        services.Scan(scan =>
        {
            scan
                .FromAssemblies(assembly)
                .AddClasses(classes => classes.AssignableTo(baseType))
                .AsSelf()
                .WithTransientLifetime()
                ;
        });
    }

    public static void RegisterAsImplementedInterfaces(
        this IServiceCollection services,
        Type interfaceType,
        Assembly assembly)
    {
        services.Scan(scan =>
        {
            scan
                .FromAssemblies(assembly)
                .AddClasses(classes => classes.AssignableTo(interfaceType), publicOnly: false)
                .AsImplementedInterfaces()
                .WithTransientLifetime()
                ;
        });
    }

    public static IServiceCollection RegisterClosedImplementationsOf(
        this IServiceCollection services,
        Type genericInterfaceType,
        Assembly assembly)
    {
        services.Scan(scan =>
        {
            scan
                .FromAssemblies(assembly)
                .AddClasses(classes => classes.AssignableTo(genericInterfaceType), publicOnly: false)
                .As(type =>
                {
                    var interfaces = type.GetInterfaces();
                    return
                        interfaces
                        .Where(i =>
                            i != genericInterfaceType &&
                            i.GetInterfaces().Any(ii => ii.IsGenericType && ii.GetGenericTypeDefinition() == genericInterfaceType)
                        );
                })
                .WithTransientLifetime()
                ;
        });

        return services;
    }
}
