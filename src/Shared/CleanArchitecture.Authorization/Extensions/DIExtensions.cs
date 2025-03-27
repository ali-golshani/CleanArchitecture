using Framework.DependencyInjection.Extensions;
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
        services.RegisterAsImplementedInterfaces(typeof(IAccessControl<>), assemblies);
    }

    public static void RegisterAllFilters(this IServiceCollection services)
    {
        var assemblies = AllAssemblies();
        services.RegisterFilters(assemblies);
    }

    public static void RegisterFilters(this IServiceCollection services)
    {
        var assembly = Assembly.GetCallingAssembly();
        services.RegisterFilters(assembly);
    }

    public static void RegisterFilters(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.RegisterAsImplementedInterfaces(typeof(IFilter<,>), assemblies);
    }
}
