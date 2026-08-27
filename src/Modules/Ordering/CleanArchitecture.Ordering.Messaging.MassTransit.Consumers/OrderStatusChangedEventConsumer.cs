using Framework.Mediator.Extensions;
using CleanArchitecture.Ordering.Commands;
using CleanArchitecture.Ordering.Queries;
using MassTransit;
using CleanArchitecture.Ordering.IntegrationEvents;
using Framework.Mediator;

namespace CleanArchitecture.Ordering.Messaging.MassTransit.Consumers;

public class OrderStatusChangedEventConsumer(ICommandService commandService, IQueryService queryService) :
    ConsumerBase(commandService, queryService),
    IConsumer<OrderStatusChangedEvent>
{
    public Task Consume(ConsumeContext<OrderStatusChangedEvent> context)
    {
        Console.WriteLine($"{GetType().Name}: Order-Id = {context.Message.OrderId}");

        var command = new Commands.DoNothings.Command
        {
            Id = context.Message.OrderId,
        };

        return Handle(command, context.CancellationToken, new RequestExecutionOptions
        {
            CorrelationId = context.Message.CorrelationId,
        });
    }
}
