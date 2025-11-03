using CleanArchitecture.Ordering.Commands.Orders.RegisterOrder;

namespace CleanArchitecture.Administration.HostedDebugApp.Services;

internal class RegisterMultipleOrdersService(IServiceProvider serviceProvider) : ServiceBase(serviceProvider)
{
    private int orderId = 0;
    private int NextOrderId => Interlocked.Increment(ref orderId);

    public virtual async Task Run()
    {
        var commandService = CommandService();
        var queryService = QueryService();

        var getOrdersResult = await queryService.Handle(new Ordering.Queries.Orders.GetOrders.Query
        {
            OrderBy = Ordering.Queries.Models.OrderOrderBy.OrderId,
            PageSize = 1
        }, default);

        if (getOrdersResult.IsFailure)
        {
            Console.WriteLine(getOrdersResult.Errors.First().Message);
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
            tasks[i] = InsertOrders(commandService);
        }

        await Task.WhenAll(tasks);

        WaitingForUserInput("Press Enter to Exit ...");
    }

    private async Task InsertOrders(Ordering.Commands.ICommandService commandService)
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

            if (result.IsFailure)
            {
                Console.WriteLine(result.Errors);
            }
        }
    }
}
