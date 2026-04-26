using CleanArchitecture.Ordering.Commands;
using CleanArchitecture.Ordering.Commands.Orders.RegisterOrder;
using CleanArchitecture.Ordering.Queries;

namespace CleanArchitecture.Administration.HostedDebugApp.Services;

internal sealed class RegisterOrderService(
    IQueryService queryService,
    ICommandService commandService) : ServiceBase
{
    public async Task RegisterNextOrder()
    {
        var getOrdersResult = await queryService.Handle(Admin, new Ordering.Queries.Orders.GetOrders.Query
        {
            OrderBy = Ordering.Queries.Models.OrderOrderBy.OrderId,
            PageSize = 1
        }, default);

        if (getOrdersResult.IsFailure)
        {
            WriteErrors(getOrdersResult);
            return;
        }

        var orders = getOrdersResult.Value!.Items;
        var order = orders.FirstOrDefault();

        var orderId = (order?.OrderId ?? 0) + 1;

        var result = await commandService.Handle(Admin, new Command
        {
            OrderId = orderId,
            BrokerId = 1,
            CommodityId = 1,
            CustomerId = 1,
            Price = 110,
            Quantity = 10,
        }, default);

        WriteErrors(result);
    }
}
