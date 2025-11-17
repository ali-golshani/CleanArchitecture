using CleanArchitecture.Authorization;
using DurableTask.Core;
using Framework.Mediator.Extensions;
using Framework.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.ProcessManager;

public static class ServiceConfigurations
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.RegisterRequestHandlers();
        services.RegisterAccessControls();
        services.RegisterValidators();

        services.AddTransient<RegisterAndApproveOrder.Handler>();
        services.AddTransient<RegisterAndApproveOrder.IService, RegisterAndApproveOrder.Service>();
        services.AddTransient<RegisterAndApproveOrder.ISchedulingService, RegisterAndApproveOrder.SchedulingService>();
    }

    public static void RegisterOrchestrations(IServiceProvider serviceProvider, TaskHubWorker worker)
    {
        RegisterAndApproveOrder.Orchestration.Register(serviceProvider, worker);
    }
}
