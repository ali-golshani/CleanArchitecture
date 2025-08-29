using CleanArchitecture.Actors;
using CleanArchitecture.Ordering.Commands;
using CleanArchitecture.Ordering.Queries;
using Framework.Mediator.BatchCommands;
using Framework.Results.Extensions;
using Framework.Scheduling;
using GetOrders = CleanArchitecture.Ordering.Queries.Orders.GetOrders;

namespace CleanArchitecture.Scheduling;

public class SampleJobService : IJobService
{
    private readonly IQueryService queryService;
    private readonly IBatchCommandsService<Ordering.Commands.SampleEmpty.Command> batchCommandsService;

    private static readonly Actor Actor = new InternalServiceActor(nameof(SampleJobService));

    public SampleJobService(
        IQueryService queryService,
        IBatchCommandsService<Ordering.Commands.SampleEmpty.Command> batchCommandsService)
    {
        this.queryService = queryService;
        this.batchCommandsService = batchCommandsService;
    }

    public async Task Execute(CancellationToken stoppingToken)
    {
        var orders = await queryService.Handle(Actor, new GetOrders.Query
        {
            OrderStatus = OrderStatus.Approved,
        }, stoppingToken)
        .ThrowIsFailure();

        var commands = orders.Items.Select(EmptyCommand).ToList();

        await batchCommandsService.Handle(commands, BatchCommandHandlingParameters.Safe, stoppingToken);
    }

    private static Ordering.Commands.SampleEmpty.Command EmptyCommand(Ordering.Queries.Models.Order order)
    {
        return new Ordering.Commands.SampleEmpty.Command
        {
            Id = order.OrderId
        };
    }
}
