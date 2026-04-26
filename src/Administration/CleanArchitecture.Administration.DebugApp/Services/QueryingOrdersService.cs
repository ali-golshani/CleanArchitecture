using CleanArchitecture.Querying.Services;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Administration.DebugApp.Services;

internal sealed class QueryingOrdersService(IQueryService queryService) : ServiceBase
{
    public async Task GetOrders()
    {
        var result = await queryService.Handle(Admin, new Querying.GetOrders.Query
        {
            CustomerId = 8
        }, default);

        if (result.IsFailure)
        {
            WriteErrors(result);
            return;
        }

        var orders = await
            result.Value!
            .OrderBy(x => x.OrderId)
            .Take(5)
            .ToListAsync();

        Console.WriteLine(orders.Count);
    }
}
