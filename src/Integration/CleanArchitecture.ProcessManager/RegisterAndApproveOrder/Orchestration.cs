using DurableTask.Core;
using Framework.Results;

namespace CleanArchitecture.ProcessManager.RegisterAndApproveOrder;

public sealed class Orchestration : TaskOrchestration<bool, Request>
{
    public static void Register(IServiceProvider serviceProvider, TaskHubWorker worker)
    {
        worker.AddTaskOrchestrations(typeof(Orchestration));
        worker.AddTaskActivitiesFromInterfaceV2<IOrchestrationService>(new OrchestrationService(serviceProvider));
    }

    private static void Write(object text)
    {
        Console.WriteLine();
        Console.WriteLine(new string('-', 80));
        Console.WriteLine(text);
    }

    public override async Task<bool> RunTask(OrchestrationContext context, Request input)
    {
        Write("Start");
        var client = context.CreateClientV2<IOrchestrationService>();

        var rollback = false;

        try
        {
            Write("A");
            var registerResult = await client.Register(input, default);
            Write("B");

            if (!registerResult)
            {
                return false;
            }

            rollback = true;

            for (int i = 0; i < 3; i++)
            {
                Write($"C {i}");
                var approveResult = await client.Approve(input, i, default);
                Write($"D {i}");
                if (approveResult)
                {
                    rollback = false;
                    return true;
                }
                await context.CreateTimer(context.CurrentUtcDateTime.AddSeconds(1), i);
            }

            Write("E");
            return false;
        }
        finally
        {
            if (rollback)
            {
                Write("F");
                await client.ControlOrderStatus(input, default);
                Write("G");
            }
        }
    }
}
