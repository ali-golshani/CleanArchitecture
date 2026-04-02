using DurableTask.Core;
using Framework.DurableTask;

namespace CleanArchitecture.ProcessManager;

public sealed class DurableTaskOrchestrationsRegistrar : IOrchestrationsRegistrar
{
    public void RegisterOrchestrations(IServiceProvider serviceProvider, TaskHubWorker worker)
    {
        ServicesConfiguration.RegisterOrchestrations(serviceProvider, worker);
    }
}
