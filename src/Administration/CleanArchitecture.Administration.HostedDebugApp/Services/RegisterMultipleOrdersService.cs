using CleanArchitecture.Ordering.Commands.Orders.RegisterOrder;

namespace CleanArchitecture.Administration.HostedDebugApp.Services;

internal class RegisterMultipleOrdersService(IServiceProvider serviceProvider) : ServiceBase(serviceProvider)
{
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

        var orderId = order?.OrderId ?? 1;

        for (int i = 0; i < 1000; i++)
        {
            var result = await commandService.Handle(new Command
            {
                OrderId = orderId + i + 1,
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

        WaitingForUserInput("Press Enter to Exit ...");
    }
}
