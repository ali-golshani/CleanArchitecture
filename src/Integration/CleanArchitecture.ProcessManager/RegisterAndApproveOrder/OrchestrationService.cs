using CleanArchitecture.Actors;
using CleanArchitecture.Ordering.Commands;
using Framework.Results;
using Framework.Results.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.ProcessManager.RegisterAndApproveOrder;

internal sealed class OrchestrationService(IServiceProvider serviceProvider) : IOrchestrationService
{
    private static readonly Programmer Actor = new("golshani", "Ali Golshani");

    private readonly IServiceProvider serviceProvider = serviceProvider;

    public async Task<SerializableResult<Empty>> Register(Request request, CancellationToken cancellationToken)
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

        return (await commandService.Handle(Actor, command, cancellationToken)).AsSerializableResult();
    }

    public async Task<SerializableResult<Empty>> Approve(Request request, int tryCount, CancellationToken cancellationToken)
    {
        if (tryCount == 0)
        {
            return new SerializableResult<Empty>
            {
                IsSuccess = false,
                Errors = ["Invalid Request"],
                CorrelationId = request.CorrelationId.ToString(),
                Value = default
            };
        }

        var commandService = GetCommandService();

        var command = new Ordering.Commands.Example.Command
        {
            Id = request.OrderId
        };

        return (await commandService.Handle(Actor, command, cancellationToken)).AsSerializableResult();
    }

    public async Task<SerializableResult<Empty>> ControlOrderStatus(Request request, CancellationToken cancellationToken)
    {
        var commandService = GetCommandService();

        var command = new Ordering.Commands.Orders.ControlOrderStatus.Command
        {
            OrderId = request.OrderId,
        };

        return (await commandService.Handle(Actor, command, cancellationToken)).AsSerializableResult();
    }

    private ICommandService GetCommandService()
    {
        return serviceProvider.GetRequiredService<ICommandService>();
    }
}