﻿using CleanArchitecture.Ordering.Application.Services;
using CleanArchitecture.Ordering.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Ordering.Application;

public static class ServiceConfigurations
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddTransient<ICommandService, CommandService>();
        services.AddTransient<IQueryService, QueryService>();
        services.AddTransient(typeof(IBatchCommandsService<>), typeof(BatchCommandsService<>));

        services.AddTransient(typeof(CommandUseCase<,>));
        services.AddTransient(typeof(QueryUseCase<,>));
    }
}