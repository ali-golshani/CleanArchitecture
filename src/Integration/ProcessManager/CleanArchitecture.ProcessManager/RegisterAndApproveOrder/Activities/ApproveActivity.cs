using CleanArchitecture.Ordering.Commands;
using Framework.DurableTask;
using Framework.DurableTask.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.ProcessManager.RegisterAndApproveOrder.Activities;

internal sealed class ApproveActivity(IServiceProvider serviceProvider) : AsyncTaskActivityBase<ApproveRequest, ActivityResult>(serviceProvider)
{
    protected override async Task<ActivityResult> ExecuteAsync(IServiceProvider serviceProvider, ApproveRequest input)
    {
        var request = input.Request;
        var tryNumber = input.TryNumber;

        if (tryNumber <= 1)
        {
            return ActivityResult.Failure(["Invalid Request"]);
        }

        var commandService = GetCommandService(serviceProvider);

        var command = new Ordering.Commands.DoNothings.Command
        {
            Id = request.OrderId
        };

        return (await commandService.Handle(Actor, command, default)).ToEmptyActivityResult();
    }

    private static ICommandService GetCommandService(IServiceProvider serviceProvider)
    {
        return serviceProvider.GetRequiredService<ICommandService>();
    }
}
