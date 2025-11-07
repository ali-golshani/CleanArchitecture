using DurableTask.Core;

namespace Framework.DurableTask;

public interface IOrchestrationsRegistrar
{
    void RegisterOrchestrations(IServiceProvider serviceProvider, TaskHubWorker worker);
}
