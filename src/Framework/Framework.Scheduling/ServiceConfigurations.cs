using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Framework.Scheduling;

public static class ServiceConfigurations
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddTransient(typeof(Job<>));
    }

    public static void RegisterHostedServices(IServiceCollection services)
    {
        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
    }

    public static void RegisterJob<TJobService>(this IServiceCollection services, string name, string cronSchedule)
        where TJobService : IJobService
    {
        services.AddQuartz(q =>
        {
            var jobKey = new JobKey(name);
            q.AddJob<Job<TJobService>>(opts => opts.WithIdentity(jobKey));
            q.AddTrigger(trigger => trigger.ForJob(jobKey).WithCronSchedule(cronSchedule));
        });
    }
}
