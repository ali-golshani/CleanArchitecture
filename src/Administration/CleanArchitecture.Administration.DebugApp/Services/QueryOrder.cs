namespace CleanArchitecture.Administration.DebugApp.Services;

public class QueryOrder(IServiceProvider serviceProvider) : ServiceBase(serviceProvider)
{
    public virtual async Task Run()
    {
        var service = QueryService();

        ResolveActor();

        var result = await service.Handle(new Ordering.Queries.OrderQuery.Query
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
