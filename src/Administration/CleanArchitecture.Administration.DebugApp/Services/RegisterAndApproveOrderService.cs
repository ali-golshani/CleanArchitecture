using RegisterAndApproveOrder = CleanArchitecture.ProcessManager.RegisterAndApproveOrder;

namespace CleanArchitecture.Administration.DebugApp.Services;

internal sealed class RegisterAndApproveOrderService(RegisterAndApproveOrder.IService service) : ServiceBase
{
    public async Task RegisterAndApproveOrder()
    {
        var result = await service.Handle(new RegisterAndApproveOrder.Request
        {
            OrderId = 1212,
            BrokerId = 5,
            CommodityId = 12,
            CustomerId = 13,
            Price = 1000,
            Quantity = 10,
        }, default);

        WriteErrors(result);
    }
}