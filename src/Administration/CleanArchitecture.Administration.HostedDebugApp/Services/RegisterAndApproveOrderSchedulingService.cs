using CleanArchitecture.Ordering.Queries;

namespace CleanArchitecture.Administration.HostedDebugApp.Services;

internal sealed class RegisterAndApproveOrderSchedulingService(
    IQueryService queryService,
    ProcessManager.RegisterAndApproveOrder.ISchedulingService schedulingService) : ServiceBase
{
    public async Task ScheduleNextOrder()
    {
        var getOrdersResult = await queryService.Handle(new Ordering.Queries.Orders.GetOrders.Query
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

        await schedulingService.Schedule(
            new ProcessManager.RegisterAndApproveOrder.Request
            {
                OrderId = orderId,
                BrokerId = 1,
                CommodityId = 1,
                CustomerId = 1,
                Price = 110,
                Quantity = 10,
            });
    }
}
