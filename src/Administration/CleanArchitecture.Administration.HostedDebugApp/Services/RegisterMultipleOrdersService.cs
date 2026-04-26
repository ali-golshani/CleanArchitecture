using CleanArchitecture.Ordering.Commands;
using CleanArchitecture.Ordering.Commands.Orders.RegisterOrder;
using CleanArchitecture.Ordering.Queries;

namespace CleanArchitecture.Administration.HostedDebugApp.Services;

internal sealed class RegisterMultipleOrdersService(
    IQueryService queryService,
    ICommandService commandService) : ServiceBase
{
    private int orderId = 0;
    private int NextOrderId => Interlocked.Increment(ref orderId);

    public async Task RegisterNextOrders()
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

        orderId = order?.OrderId ?? 1;

        Console.Write("Press Enter to start ...");
        Console.ReadLine();

        var tasks = new Task[10];
        for (int i = 0; i < tasks.Length; i++)
        {
            tasks[i] = InsertOrders();
        }

        await Task.WhenAll(tasks);
    }

    private async Task InsertOrders()
    {
        for (int i = 0; i < 100; i++)
        {
            var orderId = NextOrderId;

            var result = await commandService.Handle(new Command
            {
                OrderId = orderId,
                BrokerId = 1,
                CommodityId = 1,
                CustomerId = 1,
                Price = 110 + i * 1000,
                Quantity = 10 + i,
            }, default);

            WriteErrors(result);
        }
    }
}
