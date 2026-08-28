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
        var @event = context.Message;
        Console.WriteLine($"{GetType().Name}: Order-Id = {@event.OrderId}");

        var command = new Commands.DoNothings.Command
        {
            Id = @event.OrderId,
        };

        return Handle(command, context.CancellationToken, new RequestExecutionOptions
        {
            CorrelationId = @event.Header.CorrelationId,
        });
    }
}
