using DurableTask.Core;

namespace CleanArchitecture.Administration.HostedDebugApp.Services;

internal class TestService(IServiceProvider serviceProvider) : ServiceBase(serviceProvider)
{
    public virtual async Task Run()
    {
        var service = CommandService();
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

        var orderId = (order?.OrderId ?? 0) + 1;

        var client = Service<TaskHubClient>();
        await client.CreateOrchestrationInstanceAsync(
            typeof(ProcessManager.RegisterAndApproveOrder.Orchestration),
            new ProcessManager.RegisterAndApproveOrder.Request
            {
                OrderId = orderId,
                BrokerId = 1,
                CommodityId = 1,
                CustomerId = 1,
                Price = 110,
                Quantity = 10,
            });

        WaitingForUserInput("Press Enter to Exit ...");
    }
}
