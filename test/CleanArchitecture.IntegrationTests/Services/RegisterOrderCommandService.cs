using Framework.Results.Extensions;

namespace CleanArchitecture.IntegrationTests.Services;

internal class RegisterOrderCommandService(IServiceProvider serviceProvider) : ServiceBase(serviceProvider)
{
    public virtual async Task<bool> Run(CancellationToken cancellationToken)
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

        var result = await service.Handle(Programmer, new Ordering.Commands.RegisterOrderCommand.Command
        {
            OrderId = orderId + 1,
            BrokerId = 5,
            CommodityId = 12,
            CustomerId = 13,
            Price = 1000,
            Quantity = 10,
        }, cancellationToken)
        ;
        
        result.ThrowIsFailure();

        return true;
    }
}
