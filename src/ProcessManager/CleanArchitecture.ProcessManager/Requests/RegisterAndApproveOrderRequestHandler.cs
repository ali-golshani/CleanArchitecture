using CleanArchitecture.ProcessManager.Extensions;
using Framework.Mediator.Requests;
using Framework.ProcessManager.Extensions;
using Framework.Results;

namespace CleanArchitecture.ProcessManager.Requests;

public sealed class RegisterAndApproveOrderRequestHandler : IRequestHandler<RegisterAndApproveOrderRequest, Empty>
{
    private readonly Ordering.Commands.ICommandService commandService;

    public RegisterAndApproveOrderRequestHandler(Ordering.Commands.ICommandService commandService)
    {
        this.commandService = commandService;
    }

    public async Task<Result<Empty>> Handle(RegisterAndApproveOrderRequest request, CancellationToken cancellationToken)
    {
        var registerOrderCommand = new Ordering.Commands.RegisterOrderCommand.Command
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

        var controlCommand = new Ordering.Commands.ControlOrderStatusCommand.Command
        {
            OrderId = request.OrderId,
        };

        var processA = commandService.Process(registerOrderCommand);
        var processB = commandService.Process(emptyCommand);
        var processC = commandService.Process(controlCommand);

        var process = processA.Follow(processB).WithCompensator(processC);

        return await process.Execute(cancellationToken);
    }

    public async Task<Result<Empty>> HandleC(RegisterAndApproveOrderRequest request, CancellationToken cancellationToken)
    {
        var control = false;

        try
        {
            var registerOrderCommand = new Ordering.Commands.RegisterOrderCommand.Command
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
                var controlCommand = new Ordering.Commands.ControlOrderStatusCommand.Command
                {
                    OrderId = request.OrderId,
                };

                await commandService.Handle(controlCommand, cancellationToken);
            }
        }
    }
}
