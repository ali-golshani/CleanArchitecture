using Framework.Results.Extensions;

namespace CleanArchitecture.IntegrationTests.Services;

internal class RegisterOrderCommandService(IServiceProvider serviceProvider) : ServiceBase(serviceProvider)
{
    public virtual async Task<bool> Valid(CancellationToken cancellationToken)
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

        var customerId = Infrastructure.CommoditySystem.Mock.Data.Customers.ValidValue();
        var commodityId = Infrastructure.CommoditySystem.Mock.Data.Commodities.ValidValue();

        var result = await service.Handle(Programmer, new Ordering.Commands.RegisterOrderCommand.Command
        {
            OrderId = orderId + 1,
            CommodityId = commodityId,
            CustomerId = customerId,
            BrokerId = 5,
            Price = 1000,
            Quantity = 10,
        }, cancellationToken)
        ;

        WriteErrors(result);

        return result.IsSuccess;
    }

    public virtual async Task<bool> InvalidCommodity(CancellationToken cancellationToken)
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

        var customerId = Infrastructure.CommoditySystem.Mock.Data.Customers.ValidValue();
        var commodityId = Infrastructure.CommoditySystem.Mock.Data.Commodities.InvalidValue();

        var result = await service.Handle(Programmer, new Ordering.Commands.RegisterOrderCommand.Command
        {
            OrderId = orderId + 1,
            CommodityId = commodityId,
            CustomerId = customerId,
            BrokerId = 5,
            Price = 1000,
            Quantity = 10,
        }, cancellationToken)
        ;

        WriteErrors(result);

        return result.IsSuccess;
    }

    public virtual async Task<bool> InvalidCustomer(CancellationToken cancellationToken)
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

        var customerId = Infrastructure.CommoditySystem.Mock.Data.Customers.InvalidValue();
        var commodityId = Infrastructure.CommoditySystem.Mock.Data.Commodities.ValidValue();

        var result = await service.Handle(Programmer, new Ordering.Commands.RegisterOrderCommand.Command
        {
            OrderId = orderId + 1,
            CommodityId = commodityId,
            CustomerId = customerId,
            BrokerId = 5,
            Price = 1000,
            Quantity = 10,
        }, cancellationToken)
        ;

        WriteErrors(result);

        return result.IsSuccess;
    }
}
