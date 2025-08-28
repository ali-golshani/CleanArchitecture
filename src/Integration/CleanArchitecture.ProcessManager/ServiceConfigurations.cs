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

        services.AddKeyedPipeline<RequestPipeline.Configuration>(typeof(RequestPipeline.Pipeline<,>));

        services.AddTransient<IProcessManager, ProcessManager>();
    }
}
