using DurableTask.Core;
using Microsoft.Extensions.Hosting;

namespace Framework.DurableTask;

internal sealed class TaskHubWorkerBackgroundService(TaskHubWorker worker) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await worker.StartAsync();
        worker.TaskActivityDispatcher.IncludeDetails = true;
        worker.TaskOrchestrationDispatcher.IncludeDetails = true;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await worker.StopAsync();
    }
}
