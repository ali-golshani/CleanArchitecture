using DurableTask.Core;
using Framework.Results;

namespace CleanArchitecture.ProcessManager.RegisterAndApproveOrder;

internal sealed class Orchestration : TaskOrchestration<SerializableResult<Empty>, Request>
{
    public static void Register(IServiceProvider serviceProvider, TaskHubWorker worker)
    {
        worker.AddTaskOrchestrations(typeof(Orchestration));
        worker.AddTaskActivitiesFromInterfaceV2<IOrchestrationService>(new OrchestrationService(serviceProvider));
    }

    public override async Task<SerializableResult<Empty>> RunTask(OrchestrationContext context, Request input)
    {
        Console.WriteLine();
        Write(context, "Start Orchestration", ConsoleColor.Red);
        var client = context.CreateClientV2<IOrchestrationService>();

        var rollback = false;

        try
        {
            Write(context, "Before Register");
            var registerResult = await client.Register(input, default);
            Write(context, $"After Register: IsSuccess = {registerResult.IsSuccess}");

            if (!registerResult.IsSuccess)
            {
                return registerResult;
            }

            rollback = true;
            SerializableResult<Empty> approveResult = null!;

            for (int i = 0; i < 3; i++)
            {
                approveResult = await TryApprove(context, client, input, i);
                if (approveResult.IsSuccess)
                {
                    rollback = false;
                    return approveResult;
                }

                await context.CreateTimer(context.CurrentUtcDateTime.AddSeconds(1), i);
            }

            return approveResult!;
        }
        catch (Exception exp)
        {
            return ToResult(input, exp);
        }
        finally
        {
            if (rollback)
            {
                Write(context, "Before Rollback");
                await client.ControlOrderStatus(input, default);
                Write(context, "After Rollback");
            }
        }
    }

    private static async Task<SerializableResult<Empty>> TryApprove(
        OrchestrationContext context,
        IOrchestrationService client,
        Request request,
        int tryNumber)
    {
        try
        {
            Write(context, $"Before Approve :: Try Number = {tryNumber}");
            var result = await client.Approve(request, tryNumber, default);
            Write(context, $"After Approve :: Try Number = {tryNumber}: IsSuccess = {result.IsSuccess}");
            return result;
        }
        catch (Exception exp)
        {
            return ToResult(request, exp);
        }
    }

    private static SerializableResult<Empty> ToResult(Request request, Exception exp)
    {
        return new SerializableResult<Empty>([exp.Message], request.CorrelationId.ToString());
    }

    private static void Write(
        OrchestrationContext context,
        object text,
        ConsoleColor color = ConsoleColor.Green,
        bool logInReplay = true)
    {
        var currentColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        if (logInReplay)
        {
            Console.WriteLine($":: {text} (Replay = {context.IsReplaying})");
        }
        else if (!context.IsReplaying)
        {
            Console.WriteLine($":: {text}");
        }
        Console.ForegroundColor = currentColor;
    }
}
