using DurableTask.Core;

namespace CleanArchitecture.ProcessManager.RegisterAndApproveOrder;

internal sealed class Orchestration : TaskOrchestration<bool, Request>
{
    public static void Register(IServiceProvider serviceProvider, TaskHubWorker worker)
    {
        worker.AddTaskOrchestrations(typeof(Orchestration));
        worker.AddTaskActivitiesFromInterfaceV2<IOrchestrationService>(new OrchestrationService(serviceProvider));
    }

    public override async Task<bool> RunTask(OrchestrationContext context, Request input)
    {
        Write("Start Orchestration");
        var client = context.CreateClientV2<IOrchestrationService>();

        var rollback = false;

        try
        {
            Write("Before Register");
            await client.Register(input, default);
            Write($"After Register");

            rollback = true;

            for (int i = 0; i < 3; i++)
            {
                if (await TryApprove(client, input, i))
                {
                    rollback = false;
                    return true;
                }

                await context.CreateTimer(context.CurrentUtcDateTime.AddSeconds(1), i);
            }

            return false;
        }
        catch
        {
            return false;
        }
        finally
        {
            if (rollback)
            {
                Write("Before Rollback");
                await client.ControlOrderStatus(input, default);
                Write("After Rollback");
            }
        }
    }

    private static async Task<bool> TryApprove(IOrchestrationService client, Request input, int tryCount)
    {
        try
        {
            Write($"Before Approve :: Try Count = {tryCount}");
            await client.Approve(input, tryCount, default);
            Write($"After Approve :: Try Count = {tryCount}");
            return true;
        }
        catch
        {
            return false;
        }
    }

    private static void Write(object text)
    {
        Console.WriteLine();
        Console.Write(":: ");
        Console.WriteLine(text);
    }
}
