using Framework.Scheduling;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Scheduling;

public static class ServiceConfigurations
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.RegisterJob<SampleJobService>(nameof(SampleJobService), "0/30 * * * * ?");
    }
}
