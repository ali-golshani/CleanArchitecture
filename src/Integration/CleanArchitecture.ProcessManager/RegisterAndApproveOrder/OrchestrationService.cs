using CleanArchitecture.Actors;
using CleanArchitecture.Ordering.Commands;
using DurableTask.Core;
using Framework.Results;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.ProcessManager.RegisterAndApproveOrder;

internal sealed class OrchestrationService(IServiceProvider serviceProvider) : IOrchestrationService
{
    private static readonly Programmer Actor = new Programmer("Ali", "Ali");

    private readonly IServiceProvider serviceProvider = serviceProvider;

    public async Task<bool> Register(Request request, CancellationToken cancellationToken)
    {
        var commandService = GetCommandService();

        var command = new Ordering.Commands.Orders.RegisterOrder.Command
        {
            BrokerId = request.BrokerId,
            CommodityId = request.CommodityId,
            CustomerId = request.CustomerId,
            OrderId = request.OrderId,
            Price = request.Price,
            Quantity = request.Quantity,
        };

        var result = await commandService.Handle(Actor, command, cancellationToken);
        return result.IsSuccess;
    }

    public async Task<bool> Approve(Request request, int tryCount, CancellationToken cancellationToken)
    {
        if (tryCount == 0)
        {
            return false;
        }

        var commandService = GetCommandService();

        var command = new Ordering.Commands.Example.Command
        {
            Id = request.OrderId
        };

        return (await commandService.Handle(Actor, command, cancellationToken)).IsSuccess;
    }

    public async Task<bool> ControlOrderStatus(Request request, CancellationToken cancellationToken)
    {
        var commandService = GetCommandService();

        var command = new Ordering.Commands.Orders.ControlOrderStatus.Command
        {
            OrderId = request.OrderId,
        };

        var result = await commandService.Handle(Actor, command, cancellationToken);
        return result.IsSuccess;
    }

    private ICommandService GetCommandService()
    {
        return serviceProvider.GetRequiredService<ICommandService>();
    }
}