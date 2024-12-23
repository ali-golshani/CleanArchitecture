using CleanArchitecture.Ordering.Commands.Orders.RegisterOrderCommand;

namespace CleanArchitecture.Administration.HostedApp.Services;

internal class RegisterOrderService(IServiceProvider serviceProvider) : ServiceBase(serviceProvider)
{
    public virtual async Task Run()
    {
        var service = CommandService();

        var result = await service.Handle(new Command
        {
            OrderId = 1,
            BrokerId = 1,
            CommodityId = 1,
            CustomerId = 1,
            Price = 100,
            Quantity = 10,
        }, default);

        if (result.IsFailure)
        {
            Console.WriteLine(result.Errors);
        }

        WaitingForUserInput("Press Enter to Exit ...");
    }
}
