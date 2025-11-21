using CleanArchitecture.ProcessManager.RegisterAndApproveOrder.Activities;
using DurableTask.Core;
using Framework.DurableTask;
using Framework.DurableTask.Extensions;

namespace CleanArchitecture.ProcessManager.RegisterAndApproveOrder;

internal sealed class Orchestration : TaskOrchestration<ActivityResult, Request>
{
    public static void Register(IServiceProvider serviceProvider, TaskHubWorker worker)
    {
        worker.AddTaskOrchestrations(typeof(Orchestration));
        worker.AddTaskActivities(
            new RegisterActivity(serviceProvider),
            new ApproveActivity(serviceProvider),
            new RollbackActivity(serviceProvider));
    }

    public override async Task<ActivityResult> RunTask(OrchestrationContext context, Request input)
    {
        Console.WriteLine();
        Write(context, $"Start Orchestration {input.OrderId}", ConsoleColor.Red);

        var rollback = false;

        try
        {
            Write(context, "Before Register");
            var registerResult = await context.ScheduleTask<ActivityResult>(typeof(RegisterActivity), input);
            Write(context, $"After Register: IsSuccess = {registerResult.IsSuccess}");

            if (!registerResult.IsSuccess)
            {
                return registerResult;
            }

            rollback = true;
            ActivityResult approveResult = new();

            for (int tryNumber = 0; tryNumber < 3; tryNumber++)
            {
                approveResult = await TryApprove(context, input, tryNumber);
                if (approveResult.IsSuccess)
                {
                    rollback = false;
                    return approveResult;
                }

                await context.CreateTimer(context.CurrentUtcDateTime.AddSeconds(1), tryNumber);
            }

            return approveResult!;
        }
        catch (Exception exp)
        {
            return exp.ToEmptyActivityResult();
        }
        finally
        {
            if (rollback)
            {
                Write(context, "Before Rollback");
                await context.ScheduleTask<ActivityResult>(typeof(RollbackActivity), input);
                Write(context, "After Rollback");
            }

            Write(context, $"End Orchestration {input.OrderId}", ConsoleColor.Blue);
        }
    }

    private static async Task<ActivityResult> TryApprove(
        OrchestrationContext context,
        Request request,
        int tryNumber)
    {
        try
        {
            Write(context, $"Before Approve :: Try Number = {tryNumber}");
            var result = await context.ScheduleTask<ActivityResult>(typeof(ApproveActivity), new ApproveRequest(request, tryNumber));
            Write(context, $"After Approve :: Try Number = {tryNumber}: IsSuccess = {result.IsSuccess}");
            return result;
        }
        catch (Exception exp)
        {
            return exp.ToEmptyActivityResult();
        }
    }

    private static void Write(
        OrchestrationContext context,
        object text,
        ConsoleColor? color = null,
        bool logWhenReplaingy = true)
    {
        var currentColor = Console.ForegroundColor;
        Console.ForegroundColor = color ?? (context.IsReplaying ? ConsoleColor.Yellow : ConsoleColor.Green);
        if (logWhenReplaingy)
        {
            var replayText = context.IsReplaying ? "(Replay)" : "";
            Console.WriteLine($":: {text} {replayText}");
        }
        else if (!context.IsReplaying)
        {
            Console.WriteLine($":: {text}");
        }
        Console.ForegroundColor = currentColor;
    }
}
