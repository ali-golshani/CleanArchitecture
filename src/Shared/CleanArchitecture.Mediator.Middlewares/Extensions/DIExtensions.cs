using CleanArchitecture.Mediator.Middlewares.Transformers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanArchitecture.Mediator.Middlewares.Extensions;

public static class DIExtensions
{
    private static Assembly[] AllAssemblies() => AppDomain.CurrentDomain.GetAssemblies();

    public static void RegisterAllTransformers(this IServiceCollection services)
    {
        var assemblies = AllAssemblies();
        services.RegisterTransformers(assemblies);
    }

    public static void RegisterTransformers(this IServiceCollection services)
    {
        var assembly = Assembly.GetCallingAssembly();
        services.RegisterTransformers(assembly);
    }

    public static void RegisterTransformers(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.Scan(scan =>
        {
            scan
                .FromAssemblies(assemblies)
                .AddClasses(classes => classes.AssignableTo(typeof(ITransformer<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime()
                ;
        });
    }
}
