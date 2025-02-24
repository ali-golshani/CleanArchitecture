using Framework.Mediator.Middlewares;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Framework.Mediator.Extensions;

public static class DIExtensions
{
    private static Assembly[] AllAssemblies() => AppDomain.CurrentDomain.GetAssemblies();

    public static void RegisterAsSelf<TBase>(this IServiceCollection services)
    {
        var assembly = Assembly.GetCallingAssembly();

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

    public static void RegisterAllRequestHandlers(this IServiceCollection services)
    {
        var assemblies = AllAssemblies();
        services.RegisterRequestHandlers(assemblies);
    }

    public static void RegisterRequestHandlers(this IServiceCollection services)
    {
        var assembly = Assembly.GetCallingAssembly();
        services.RegisterRequestHandlers(assembly);
    }

    public static void RegisterRequestHandlers(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.Scan(scan =>
        {
            scan
                .FromAssemblies(assemblies)
                .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<,>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime()
                ;
        });
    }

    public static void RegisterMiddlewares<TPipelineConfiguration>(this IServiceCollection services)
        where TPipelineConfiguration : IKeyedPipelineConfiguration
    {
        foreach (var type in TPipelineConfiguration.Middlewares())
        {
            services.AddKeyedTransient(typeof(IMiddleware<,>), TPipelineConfiguration.PipelineName, type);
        }
    }
}
