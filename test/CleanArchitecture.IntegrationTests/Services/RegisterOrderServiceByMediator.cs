using CleanArchitecture.Ordering.Queries.Orders.OrdersQuery;
using Framework.Results;
using Framework.Results.Extensions;

namespace CleanArchitecture.IntegrationTests.Services;

internal class RegisterOrderServiceByMediator(IServiceProvider serviceProvider) : ServiceBase(serviceProvider)
{
    public async Task<Result<Empty>> Register(int customerId, int commodityId, CancellationToken cancellationToken)
    {
        var mediator = Mediator();

        ResolveActor();
        var ordersResult = await mediator.Send(new Query
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

        return await mediator.Send(command, cancellationToken);
    }
}
