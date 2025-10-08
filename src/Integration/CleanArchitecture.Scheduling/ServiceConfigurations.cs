using Framework.Scheduling;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Scheduling;

public static class ServiceConfigurations
{
    public static void RegisterJobs(IServiceCollection services)
    {
        services.RegisterJob<SampleJobService>(nameof(SampleJobService), "0/30 * * * * ?");
    }
}
