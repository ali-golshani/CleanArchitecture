using DurableTask.Core;

namespace CleanArchitecture.ProcessManager.RegisterAndApproveOrder;

internal sealed class SchedulingService(TaskHubClient client) : ISchedulingService
{
    private readonly TaskHubClient client = client;

    public async Task Schedule(Request request)
    {
        await client.CreateOrchestrationInstanceAsync(typeof(Orchestration), request);
    }
}