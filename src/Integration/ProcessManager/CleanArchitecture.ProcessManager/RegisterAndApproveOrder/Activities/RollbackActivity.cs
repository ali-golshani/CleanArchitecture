using CleanArchitecture.Ordering.Commands;
using Framework.DurableTask;
using Framework.DurableTask.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.ProcessManager.RegisterAndApproveOrder.Activities;

internal sealed class RollbackActivity(IServiceProvider serviceProvider) : AsyncTaskActivityBase<Request, ActivityResult>(serviceProvider)
{
    protected override async Task<ActivityResult> ExecuteAsync(IServiceProvider serviceProvider, Request input)
    {
        var commandService = GetCommandService(serviceProvider);

        var command = new Ordering.Commands.Orders.ControlOrderStatus.Command
        {
            OrderId = input.OrderId,
        };

        return (await commandService.Handle(Actor, command, default)).ToEmptyActivityResult();
    }

    private static ICommandService GetCommandService(IServiceProvider serviceProvider)
    {
        return serviceProvider.GetRequiredService<ICommandService>();
    }
}
