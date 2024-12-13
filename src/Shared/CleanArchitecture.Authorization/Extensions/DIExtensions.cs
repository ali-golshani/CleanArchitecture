using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanArchitecture.Authorization;

public static class DIExtensions
{
    private static Assembly[] AllAssemblies() => AppDomain.CurrentDomain.GetAssemblies();

    public static void RegisterAllAccessControls(this IServiceCollection services)
    {
        var assemblies = AllAssemblies();
        RegisterAccessControls(services, assemblies);
    }

    public static void RegisterAccessControls(this IServiceCollection services)
    {
        var assembly = Assembly.GetCallingAssembly();
        RegisterAccessControls(services, assembly);
    }

    public static void RegisterAccessControls(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.Scan(scan =>
        {
            scan
                .FromAssemblies(assemblies)
                .AddClasses(classes => classes.AssignableTo(typeof(IAccessControl<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime()
                ;
        });
    }

    public static void RegisterAllQueryFilters(this IServiceCollection services)
    {
        var assemblies = AllAssemblies();
        RegisterQueryFilters(services, assemblies);
    }

    public static void RegisterQueryFilters(this IServiceCollection services)
    {
        var assembly = Assembly.GetCallingAssembly();
        RegisterQueryFilters(services, assembly);
    }

    public static void RegisterQueryFilters(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.Scan(scan =>
        {
            scan
                .FromAssemblies(assemblies)
                .AddClasses(classes => classes.AssignableTo(typeof(IQueryFilter<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime()
                ;
        });
    }

    public static void RegisterAllDataFilters(this IServiceCollection services)
    {
        var assemblies = AllAssemblies();
        RegisterDataFilters(services, assemblies);
    }

    public static void RegisterDataFilters(this IServiceCollection services)
    {
        var assembly = Assembly.GetCallingAssembly();
        RegisterDataFilters(services, assembly);
    }

    public static void RegisterDataFilters(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.Scan(scan =>
        {
            scan
                .FromAssemblies(assemblies)
                .AddClasses(classes => classes.AssignableTo(typeof(IDataFilter<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime()
                ;
        });
    }
}
