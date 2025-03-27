using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Administration.DebugApp.Services;

internal class QueryingOrdersService(IServiceProvider serviceProvider) : ServiceBase(serviceProvider)
{
    public virtual async Task Run()
    {
        var service = Service<Querying.Services.IQueryService>();

        ResolveActor();

        var result = await service.Handle(new Querying.OrdersQuery.Query
        {
            CustomerId = 8
        }, default);

        if (result.IsFailure)
        {
            Console.WriteLine(result.Errors);
        }

        var orders = await
            result.Value!
            .OrderBy(x => x.OrderId)
            .Take(5)
            .ToListAsync();

        Console.WriteLine(orders.Count);

        WaitingForUserInput("Press Enter to Exit ...");
    }
}
