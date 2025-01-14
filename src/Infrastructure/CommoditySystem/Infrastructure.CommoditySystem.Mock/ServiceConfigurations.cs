﻿using Framework.Mediator.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.CommoditySystem.Mock;

public static class ServiceConfigurations
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.RegisterRequestHandlers();
        services.AddScoped<ICommoditySystem, CommoditySystem>();
    }
}
