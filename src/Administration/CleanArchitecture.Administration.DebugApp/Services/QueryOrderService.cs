using CleanArchitecture.Ordering.Queries.Orders.OrderQuery;

namespace CleanArchitecture.Administration.DebugApp.Services;

internal class QueryOrderService(IServiceProvider serviceProvider) : ServiceBase(serviceProvider)
{
    public virtual async Task Run()
    {
        var service = QueryService();

        ResolveActor();

        var result = await service.Handle(new Query
        {
            OrderId = 14
        }, default);

        if (result.IsFailure)
        {
            Console.WriteLine(result.Errors);
        }

        Console.WriteLine(result.Value?.OrderId);

        WaitingForUserInput("Press Enter to Exit ...");
    }
}
