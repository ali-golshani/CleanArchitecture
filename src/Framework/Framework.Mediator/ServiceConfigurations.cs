﻿using Framework.Mediator.DomainEvents;
using Framework.Mediator.IntegrationEvents;
using Framework.Mediator.Requests;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Mediator;

public static class ServiceConfigurations
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddTransient(typeof(DomainEventPublisher<>));
        services.AddTransient<IDomainEventPublisher, DomainEventPublisher>();
        services.AddScoped<IIntegrationEventBus, IntegrationEventBus>();
        services.AddTransient<IRequestMediator, RequestMediator>();
    }
}
