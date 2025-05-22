using Framework.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanArchitecture.Authorization;

public static class DIExtensions
{
    public static void RegisterAccessControls(this IServiceCollection services)
    {
        services.RegisterAccessControls(Assembly.GetCallingAssembly());
    }

    public static void RegisterAccessControls(this IServiceCollection services, Assembly assembly)
    {
        services.RegisterAsImplementedInterfaces(typeof(IAccessControl<>), assembly);
    }

    public static void RegisterFilters(this IServiceCollection services)
    {
        services.RegisterFilters(Assembly.GetCallingAssembly());
    }

    public static void RegisterFilters(this IServiceCollection services, Assembly assembly)
    {
        services.RegisterAsImplementedInterfaces(typeof(IFilter<,>), assembly);
    }
}
