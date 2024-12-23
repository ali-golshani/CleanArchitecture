using CleanArchitecture.ProcessManager.Extensions;
using Framework.Mediator.Requests;
using Framework.ProcessManager.Extensions;
using Framework.Results;
using Command = CleanArchitecture.Ordering.Commands.Orders.RegisterOrderCommand.Command;

namespace CleanArchitecture.ProcessManager.Handlers;

internal sealed class Handler : IRequestHandler<RegisterAndApproveOrder.Request, Empty>
{
    private readonly Ordering.Commands.ICommandService commandService;

    public Handler(Ordering.Commands.ICommandService commandService)
    {
        this.commandService = commandService;
    }

    public async Task<Result<Empty>> Handle(RegisterAndApproveOrder.Request request, CancellationToken cancellationToken)
    {
        var registerOrderCommand = new Command
        {
            BrokerId = request.BrokerId,
            CommodityId = request.CommodityId,
            CustomerId = request.CustomerId,
            OrderId = request.OrderId,
            Price = request.Price,
            Quantity = request.Quantity,
        };

        var emptyCommand = new Ordering.Commands.EmptyTestingCommand.Command
        {
            Id = request.OrderId
        };

        var controlCommand = new Ordering.Commands.Orders.ControlOrderStatusCommand.Command
        {
            OrderId = request.OrderId,
        };

        var registerOrderProcess = commandService.Process(registerOrderCommand);
        var emptyProcess = commandService.Process(emptyCommand);
        var controlProcess = commandService.Process(controlCommand);

        var process = registerOrderProcess.Follow(emptyProcess).WithCompensator(controlProcess);

        return await process.Execute(cancellationToken);
    }

    public async Task<Result<Empty>> HandleC(RegisterAndApproveOrder.Request request, CancellationToken cancellationToken)
    {
        var control = false;

        try
        {
            var registerOrderCommand = new Command
            {
                BrokerId = request.BrokerId,
                CommodityId = request.CommodityId,
                CustomerId = request.CustomerId,
                OrderId = request.OrderId,
                Price = request.Price,
                Quantity = request.Quantity,
            };

            var registerResult = await commandService.Handle(registerOrderCommand, cancellationToken);

            if (registerResult.IsFailure)
            {
                return registerResult;
            }

            control = true;

            var otherCommand = new Ordering.Commands.EmptyTestingCommand.Command
            {
                Id = request.OrderId
            };

            var otherResult = await commandService.Handle(otherCommand, cancellationToken);

            control = otherResult.IsFailure;

            return otherResult;
        }
        finally
        {
            if (control)
            {
                var controlCommand = new Ordering.Commands.Orders.ControlOrderStatusCommand.Command
                {
                    OrderId = request.OrderId,
                };

                await commandService.Handle(controlCommand, cancellationToken);
            }
        }
    }
}
