﻿using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanArchitecture.Authorization;

public static class DIExtensions
{
    private static Assembly[] AllAssemblies() => AppDomain.CurrentDomain.GetAssemblies();

    public static void RegisterAllAccessVerifiers(this IServiceCollection services)
    {
        var assemblies = AllAssemblies();
        RegisterAccessVerifiers(services, assemblies);
    }

    public static void RegisterAccessVerifiers(this IServiceCollection services)
    {
        var assembly = Assembly.GetCallingAssembly();
        RegisterAccessVerifiers(services, assembly);
    }

    public static void RegisterAccessVerifiers(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.Scan(scan =>
        {
            scan
                .FromAssemblies(assemblies)
                .AddClasses(classes => classes.AssignableTo(typeof(IAccessVerifier<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime()
                ;
        });
    }
}
