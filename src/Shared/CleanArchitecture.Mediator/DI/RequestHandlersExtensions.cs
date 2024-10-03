using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanArchitecture.Mediator.DependencyInjection;

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
        RegisterRequestHandlers(services, assemblies);
    }

    public static void RegisterRequestHandlers(this IServiceCollection services)
    {
        var assembly = Assembly.GetCallingAssembly();
        RegisterRequestHandlers(services, assembly);
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

    public static void RegisterRequestUseCases(this IServiceCollection services, Type useCaseType, Type requestType)
    {
        var assembly = requestType.Assembly;

        var types =
            assembly.GetTypes()
            .Where(x => x.IsNonAbstractClass(true))
            .ToList();

        var requests = new List<Type>();

        foreach (var type in types)
        {
            if (!type.IsBasedOn(requestType))
            {
                continue;
            }

            if (type.IsBasedOn(typeof(IRequest<,>)))
            {
                requests.Add(type);
            }
        }

        foreach (var request in requests)
        {
            var irequest = request.IRequestType();

            if (irequest is null)
            {
                continue;
            }

            var genericArguments = irequest.GetGenericArguments();

            var iUseCase = typeof(IUseCase<,>).MakeGenericType(genericArguments);
            var useCase = useCaseType.MakeGenericType(genericArguments);

            services.AddScoped(iUseCase, useCase);
        }
    }

    private static Type? IRequestType(this Type type)
    {
        var implementedInterfaces = type.GetInterfaces();
        foreach (var implementedInterface in implementedInterfaces)
        {
            if (implementedInterface.IsAssignableToGenericTypeDefinition(typeof(IRequest<,>)))
            {
                return implementedInterface;
            }
        }
        return null;
    }
}
