using CleanArchitecture.Actors;
using CleanArchitecture.Ordering.Commands;
using Framework.Results.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.ProcessManager.RegisterAndApproveOrder;

internal sealed class OrchestrationService(IServiceProvider serviceProvider) : IOrchestrationService
{
    private static readonly Programmer Actor = new("golshani", "Ali Golshani");

    private readonly IServiceProvider serviceProvider = serviceProvider;

    public async Task Register(Request request, CancellationToken cancellationToken)
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

        await commandService.Handle(Actor, command, cancellationToken).ThrowIsFailure();
    }

    public async Task Approve(Request request, int tryCount, CancellationToken cancellationToken)
    {
        if (tryCount == 0)
        {
            throw new InvalidOperationException();
        }

        var commandService = GetCommandService();

        var command = new Ordering.Commands.Example.Command
        {
            Id = request.OrderId
        };

        await commandService.Handle(Actor, command, cancellationToken).ThrowIsFailure();
    }

    public async Task ControlOrderStatus(Request request, CancellationToken cancellationToken)
    {
        var commandService = GetCommandService();

        var command = new Ordering.Commands.Orders.ControlOrderStatus.Command
        {
            OrderId = request.OrderId,
        };

        await commandService.Handle(Actor, command, cancellationToken).ThrowIsFailure();
    }

    private ICommandService GetCommandService()
    {
        return serviceProvider.GetRequiredService<ICommandService>();
    }
}