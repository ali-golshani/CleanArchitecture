using DurableTask.Core;
using Framework.Results;

namespace CleanArchitecture.ProcessManager.RegisterAndApproveOrder;

internal sealed class Orchestration : TaskOrchestration<Result<Empty>, Request>
{
    public static void Register(IServiceProvider serviceProvider, TaskHubWorker worker)
    {
        worker.AddTaskOrchestrations(typeof(Orchestration));
        worker.AddTaskActivitiesFromInterfaceV2<IOrchestrationService>(new OrchestrationService(serviceProvider));
    }

    public override async Task<Result<Empty>> RunTask(OrchestrationContext context, Request input)
    {
        var client = context.CreateClientV2<IOrchestrationService>();

        var control = false;

        try
        {
            var registerResult = await client.Register(input, default);

            if (registerResult.IsFailure)
            {
                return registerResult;
            }

            control = true;

            var otherResult = await client.Register(input, default);

            control = otherResult.IsFailure;

            return otherResult;
        }
        finally
        {
            if (control)
            {
                await client.ControlOrderStatus(input, default);
            }
        }
    }
}
