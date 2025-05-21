using CleanArchitecture.Authorization;
using CleanArchitecture.ProcessManager.Pipelines;
using Framework.Mediator.Extensions;
using Framework.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.ProcessManager;

public static class ServiceConfigurations
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.RegisterRequestHandlers();
        services.RegisterDomainEventHandlers();
        services.RegisterAccessControls();
        services.RegisterValidators();

        services.AddTransient(typeof(RequestPipeline.Pipeline<,>));
        services.RegisterMiddlewares<RequestPipeline.Configuration>();

        services.AddTransient<IProcessManager, ProcessManager>();
    }
}
