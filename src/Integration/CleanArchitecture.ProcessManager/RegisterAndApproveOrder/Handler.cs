using Framework.Mediator;
using Framework.Results;

namespace CleanArchitecture.ProcessManager.RegisterAndApproveOrder;

internal sealed class Handler(Ordering.Commands.ICommandService commandService) : IRequestHandler<Request, Empty>
{
    private readonly Ordering.Commands.ICommandService commandService = commandService;

    public async Task<Result<Empty>> Handle(Request request, CancellationToken cancellationToken)
    {
        var control = false;

        try
        {
            var registerOrderCommand = new Ordering.Commands.Orders.RegisterOrder.Command
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

            var otherCommand = new Ordering.Commands.Example.Command
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
                var controlCommand = new Ordering.Commands.Orders.ControlOrderStatus.Command
                {
                    OrderId = request.OrderId,
                };

                await commandService.Handle(controlCommand, cancellationToken);
            }
        }
    }
}
