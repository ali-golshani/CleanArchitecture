namespace CleanArchitecture.IntegrationTests.Services;

public class RegisterOrderCommandService(IServiceProvider serviceProvider) : ServiceBase(serviceProvider)
{
    public virtual async Task<bool> Run()
    {
        var service = CommandService();

        var result = await service.Handle(Programmer, new Ordering.Commands.RegisterOrderCommand.Command
        {
            OrderId = 1020,
            BrokerId = 5,
            CommodityId = 12,
            CustomerId = 13,
            Price = 1000,
            Quantity = 10,
        }, default);

        return result.IsSuccess;
    }
}
