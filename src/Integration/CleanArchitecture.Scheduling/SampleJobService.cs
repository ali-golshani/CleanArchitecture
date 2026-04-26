using CleanArchitecture.Actors;
using CleanArchitecture.Ordering.Commands;
using CleanArchitecture.Ordering.Queries;
using Framework.Mediator.BatchCommands;
using Framework.Results.Extensions;
using Framework.Scheduling;
using GetOrders = CleanArchitecture.Ordering.Queries.Orders.GetOrders;
using DoNothings = CleanArchitecture.Ordering.Commands.DoNothings;

namespace CleanArchitecture.Scheduling;

public sealed class SampleJobService : IJobService
{
    private readonly IQueryService queryService;
    private readonly IBatchCommandsService<DoNothings.Command> batchCommandsService;

    private static readonly Actor Actor = new InternalServiceActor(nameof(SampleJobService));

    public SampleJobService(
        IQueryService queryService,
        IBatchCommandsService<DoNothings.Command> batchCommandsService)
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

        var commands = orders.Items.Select(ExampleCommand).ToList();

        await batchCommandsService.Handle(commands, BatchCommandHandlingParameters.Safe, stoppingToken);
    }

    private static DoNothings.Command ExampleCommand(Ordering.Queries.Models.Order order)
    {
        return new DoNothings.Command
        {
            Id = order.OrderId
        };
    }
}
