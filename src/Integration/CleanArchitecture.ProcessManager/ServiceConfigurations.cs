using CleanArchitecture.Authorization;
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
        services.RegisterAccessVerifiers();
        services.RegisterValidators();

        services.AddTransient<IProcessManager, ProcessService>();
    }
}
