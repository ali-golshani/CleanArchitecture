using Framework.Mediator.Requests;
using Framework.Results;
using CleanArchitecture.ProcessManager.Processes;

namespace CleanArchitecture.ProcessManager.Requests.RegisterAndApproveOrder;

public sealed class Handler : IRequestHandler<Request, Empty>
{
    private readonly Ordering.Commands.ICommandService commandService;

    public Handler(Ordering.Commands.ICommandService commandService)
    {
        this.commandService = commandService;
    }

    public async Task<Result<Empty>> Handle(Request request, CancellationToken cancellationToken)
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

        var process =
            commandService.Process(registerOrderCommand)
            .Follow(_ =>
            {
                return new Ordering.Commands.EmptyTestingCommand.Command
                {
                    Id = request.OrderId
                };
            })
            .WithCompensator(_ =>
            {
                return commandService.Process(new Ordering.Commands.ControlOrderStatusCommand.Command
                {
                    OrderId = request.OrderId,
                });
            });

        return await process.Execute(cancellationToken);

        //var control = false;

        //try
        //{
        //    var registerOrderCommand = new Ordering.Commands.RegisterOrderCommand.Command
        //    {
        //        BrokerId = request.BrokerId,
        //        CommodityId = request.CommodityId,
        //        CustomerId = request.CustomerId,
        //        OrderId = request.OrderId,
        //        Price = request.Price,
        //        Quantity = request.Quantity,
        //    };

        //    var registerResult = await commandService.Handle(registerOrderCommand, cancellationToken);

        //    if (registerResult.IsFailure)
        //    {
        //        return registerResult;
        //    }

        //    control = true;

        //    var otherCommand = new Ordering.Commands.EmptyTestingCommand.Command
        //    {
        //        Id = request.OrderId
        //    };

        //    var otherResult = await commandService.Handle(otherCommand, cancellationToken);

        //    control = otherResult.IsFailure;

        //    return otherResult;
        //}
        //finally
        //{
        //    if (control)
        //    {
        //        var controlCommand = new Ordering.Commands.ControlOrderStatusCommand.Command
        //        {
        //            OrderId = request.OrderId,
        //        };

        //        await commandService.Handle(controlCommand, cancellationToken);
        //    }
        //}
    }
}
