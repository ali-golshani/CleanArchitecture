﻿using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanArchitecture.Mediator.Middlewares.Extensions;

public static class DIExtensions
{
    private static Assembly[] AllAssemblies() => AppDomain.CurrentDomain.GetAssemblies();

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
        services.Scan(scan =>
        {
            scan
                .FromAssemblies(assemblies)
                .AddClasses(classes => classes.AssignableTo(typeof(IFilter<,>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime()
                ;
        });
    }
}