using DurableTask.Core;
using Microsoft.Extensions.Hosting;

namespace Framework.DurableTask;

internal sealed class TaskHubWorkerBackgroundService(TaskHubWorker worker) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        return worker.StartAsync();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return worker.StopAsync();
    }
}
