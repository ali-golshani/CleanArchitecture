using CleanArchitecture.Ordering.Commands;
using Framework.Results;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.ProcessManager.RegisterAndApproveOrder;

internal sealed class OrchestrationService(IServiceProvider serviceProvider) : IOrchestrationService
{
    private readonly IServiceProvider serviceProvider = serviceProvider;

    public async Task<Result<Empty>> Register(Request request, CancellationToken cancellationToken)
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

        return await commandService.Handle(command, cancellationToken);
    }

    public async Task<Result<Empty>> Approve(Request request, CancellationToken cancellationToken)
    {
        var commandService = GetCommandService();

        var command = new Ordering.Commands.Example.Command
        {
            Id = request.OrderId
        };

        return await commandService.Handle(command, cancellationToken);
    }

    public async Task<Result<Empty>> ControlOrderStatus(Request request, CancellationToken cancellationToken)
    {
        var commandService = GetCommandService();

        var command = new Ordering.Commands.Orders.ControlOrderStatus.Command
        {
            OrderId = request.OrderId,
        };

        return await commandService.Handle(command, cancellationToken);
    }

    private ICommandService GetCommandService()
    {
        return serviceProvider.GetRequiredService<ICommandService>();
    }
}