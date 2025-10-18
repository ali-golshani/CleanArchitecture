using Framework.Mediator.Extensions;
using CleanArchitecture.Ordering.Commands;
using CleanArchitecture.Ordering.Queries;
using MassTransit;
using CleanArchitecture.Ordering.IntegrationEvents;

namespace CleanArchitecture.Ordering.Application.MassTransit.Consumers;

public class OrderStatusChangedEventConsumer(ICommandService commandService, IQueryService queryService) :
    ConsumerBase(commandService, queryService),
    IConsumer<OrderStatusChangedEvent>
{
    public Task Consume(ConsumeContext<OrderStatusChangedEvent> context)
    {
        Console.WriteLine($"{GetType().Name}: Order-Id = {context.Message.OrderId}");

        var command = new Commands.Example.Command
        {
            Id = context.Message.OrderId,
        }
        .WithCorrelationId(context.Message.CorrelationId);

        return Handle(command, context.CancellationToken);
    }
}
