using RegisterAndApproveOrder = CleanArchitecture.ProcessManager.RegisterAndApproveOrder;

namespace CleanArchitecture.Administration.DebugApp.Services;

internal class RegisterAndApproveOrderService(IServiceProvider serviceProvider) : ServiceBase(serviceProvider)
{
    public virtual async Task Run()
    {
        var service = Service<RegisterAndApproveOrder.IService>();
        ResolveActor();

        var result = await service.Handle(new RegisterAndApproveOrder.Request
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
