using CleanArchitecture.ProcessManager;

namespace CleanArchitecture.Administration.DebugApp.Services;

public class RegisterAndApproveOrderService(IServiceProvider serviceProvider) : ServiceBase(serviceProvider)
{
    public virtual async Task Run()
    {
        var service = Service<IProcessManager>();
        ResolveActor();

        var result = await service.Handle(new ProcessManager.RegisterAndApproveOrder.Request
        {
            OrderId = 1212,
            BrokerId = 5,
            CommodityId = 12,
            CustomerId = 13,
            Price = 1000,
            Quantity = 10,
        }, default);

        if (result.IsFailure)
        {
            Console.WriteLine(result.Errors);
        }

        WaitingForUserInput("Press Enter to Exit ...");
    }
}
