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
}
