using Framework.Results;
using Framework.Results.Extensions;
using GetOrders = CleanArchitecture.Ordering.Queries.Orders.GetOrders;

namespace CleanArchitecture.IntegrationTests.Services;

internal class RegisterOrderService(IServiceProvider serviceProvider) : ServiceBase(serviceProvider)
{
    public async Task<Result<Empty>> Register(int customerId, int commodityId, CancellationToken cancellationToken)
    {
        var queryService = QueryService();
        var service = CommandService();

        var ordersResult = await queryService.Handle(Programmer, new GetOrders.Query
        {
            OrderBy = Ordering.Queries.Models.OrderOrderBy.OrderId,
            PageSize = 1
        }, cancellationToken)
        ;

        var orders = ordersResult.ThrowIsFailure();

        var orderId = orders.Items.Count > 0 ? orders.Items.Max(x => x.OrderId) : 1;

        var command = new Fakers.RegisterOrderCommandFaker
        (
            orderId: orderId + 1,
            customerId: customerId,
            commodityId: commodityId
        ).Generate();

        return await service.Handle(Programmer, command, cancellationToken);
    }
}
