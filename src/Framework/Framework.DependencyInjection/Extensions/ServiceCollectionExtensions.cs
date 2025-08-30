//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Linq;
//using System.Reflection;

//public static class ServiceCollectionExtensions
//{
//    public static IServiceCollection AddUseCases(this IServiceCollection services, Assembly assembly)
//    {
//        services.Scan(scan => scan
//            .FromAssemblies(assembly)
//            .AddClasses(c => c.AssignableTo(typeof(IUseCase<,>)))
//            .As(c =>
//            {
//                // pick the "special interface", not the open generic
//                var interfaces = c.GetInterfaces();
//                return interfaces
//                    .Where(i =>
//                        i != typeof(IUseCase<,>) &&
//                        i.GetInterfaces().Any(ii =>
//                            ii.IsGenericType && ii.GetGenericTypeDefinition() == typeof(IUseCase<,>))
//                    );
//            })
//            .WithScopedLifetime());

//        return services;
//    }
//}
