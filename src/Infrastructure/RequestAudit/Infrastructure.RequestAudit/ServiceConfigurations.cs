﻿using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.RequestAudit;

public static class ServiceConfigurations
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddSingleton<RequestAuditAgent>();
    }
}
