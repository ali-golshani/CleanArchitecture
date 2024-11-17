using Framework.Mediator.Requests;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Framework.Mediator.Extensions;

public static class RequestHandlersExtensions
{
    private static Assembly[] AllAssemblies() => AppDomain.CurrentDomain.GetAssemblies();

    public static IRequestHandler<TRequest, TResponse> RequestHandler<TRequest, TResponse>(this IServiceProvider serviceProvider)
        where TRequest : IRequest<TRequest, TResponse>
    {
        return serviceProvider.GetRequiredService<IRequestHandler<TRequest, TResponse>>();
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
}
