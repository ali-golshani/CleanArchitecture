using Microsoft.Extensions.DependencyInjection;

namespace Framework.Scheduling;

public static class Extensions
{
    public static async Task RunJob<TJobService>(
        this IServiceProvider serviceProvider,
        CancellationToken cancellationToken)
        where TJobService : IJobService
    {
        var job = serviceProvider.GetRequiredService<Job<TJobService>>();
        await job.InternalRun(cancellationToken);
    }
}
