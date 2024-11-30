using Framework.Results;
using Framework.Results.Extensions;

namespace CleanArchitecture.IntegrationTests.Services;

internal class RegisterOrderService(IServiceProvider serviceProvider) : ServiceBase(serviceProvider)
{
    public async Task<Result<Empty>> Register(int customerId, int commodityId, CancellationToken cancellationToken)
    {
        var queryService = QueryService();
        var service = CommandService();

        var ordersResult = await queryService.Handle(Programmer, new Ordering.Queries.OrdersQuery.Query
        {
            OrderBy = Ordering.Queries.Models.OrderOrderBy.OrderId,
            PageSize = 1
        }, cancellationToken)
        ;

        var orders = ordersResult.ThrowIsFailure();

        var orderId = orders.Items.Count > 0 ? orders.Items.Max(x => x.OrderId) : 1;

        return await service.Handle(Programmer, new Ordering.Commands.RegisterOrderCommand.Command
        {
            OrderId = orderId + 1,
            CommodityId = commodityId,
            CustomerId = customerId,
            BrokerId = 5,
            Price = 1000,
            Quantity = 10,
        }, cancellationToken);
    }
}
