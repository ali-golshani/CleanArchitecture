using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Framework.Scheduling;

internal sealed class Job<TJobService> : IJob
    where TJobService : IJobService
{
    private readonly IServiceScopeFactory serviceScopeFactory;
    private readonly ILogger logger;

    public Job(IServiceScopeFactory serviceScopeFactory, ILogger<Job<TJobService>> logger)
    {
        this.serviceScopeFactory = serviceScopeFactory;
        this.logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var job = context.JobDetail.Key.Name;

        try
        {
            BeforeExecuting(job);
            await InternalRun(context.CancellationToken);
            OnSuccessful(job);
        }
        catch (Exception exp)
        {
            OnFailure(job, exp);
        }
    }

    internal async Task InternalRun(CancellationToken cancellationToken)
    {
        using var scope = serviceScopeFactory.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<TJobService>();
        await service.Execute(cancellationToken);
    }

    private void BeforeExecuting(string job)
    {
        logger.LogInformation("Job '{@Name}' Executing", job);
    }

    private void OnSuccessful(string job)
    {
        logger.LogInformation("Job '{@Name}' Executed Successfully", job);
    }

    private void OnFailure(string job, Exception exp)
    {
        logger.LogError(exp, "Job '{@Name}' Failed", job);
    }
}
