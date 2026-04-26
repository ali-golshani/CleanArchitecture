using CleanArchitecture.Ordering.Commands;

namespace CleanArchitecture.Administration.DebugApp.Services;

internal sealed class RegisterOrderService(ICommandService commandService) : ServiceBase
{
    public async Task RegisterOrder()
    {
        var result = await commandService.Handle(Admin, new Ordering.Commands.Orders.RegisterOrder.Command
        {
            OrderId = 165,
            BrokerId = 5,
            CommodityId = 12,
            CustomerId = 13,
            Price = 1000,
            Quantity = 10,
        }, default);

        WriteErrors(result);
    }
}
