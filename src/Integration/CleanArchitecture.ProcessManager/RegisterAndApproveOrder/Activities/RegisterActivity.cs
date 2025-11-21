using CleanArchitecture.Ordering.Commands;
using Framework.DurableTask;
using Framework.DurableTask.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.ProcessManager.RegisterAndApproveOrder.Activities;

internal sealed class RegisterActivity(IServiceProvider serviceProvider) : AsyncTaskActivityBase<Request, ActivityResult>(serviceProvider)
{
    protected override async Task<ActivityResult> ExecuteAsync(IServiceProvider serviceProvider, Request input)
    {
        var commandService = GetCommandService(serviceProvider);

        var command = new Ordering.Commands.Orders.RegisterOrder.Command
        {
            OrderId = input.OrderId,
            BrokerId = input.BrokerId,
            CustomerId = input.CustomerId,
            CommodityId = input.CommodityId,
            Price = input.Price,
            Quantity = input.Quantity,
        };

        return (await commandService.Handle(Actor, command, default)).ToEmptyActivityResult();
    }

    private static ICommandService GetCommandService(IServiceProvider serviceProvider)
    {
        return serviceProvider.GetRequiredService<ICommandService>();
    }
}
