﻿using CleanArchitecture.Ordering.Commands.Orders.RegisterOrderCommand;

namespace CleanArchitecture.Administration.DebugApp.Services;

internal class RegisterOrderCommandService(IServiceProvider serviceProvider) : ServiceBase(serviceProvider)
{
    public virtual async Task Run()
    {
        var service = CommandService();

        var result = await service.Handle(Actor, new Command
        {
            OrderId = 101,
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
