using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.ProcessManager;

public static class ServicesRegistration
{
    public static void AddProcessManagerModule(this IServiceCollection services)
    {
        ServicesConfiguration.RegisterServices(services);
    }

}
