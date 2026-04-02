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

        if (tryNumber == 0)
        {
            return new ActivityResult(["Invalid Request"]);
        }

        var commandService = GetCommandService(serviceProvider);

        var command = new Ordering.Commands.Example.Command
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
