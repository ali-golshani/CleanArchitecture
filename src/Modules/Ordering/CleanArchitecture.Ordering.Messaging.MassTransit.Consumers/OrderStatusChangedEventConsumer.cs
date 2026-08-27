using Framework.Mediator.Extensions;
using CleanArchitecture.Ordering.Commands;
using CleanArchitecture.Ordering.Queries;
using MassTransit;
using CleanArchitecture.Ordering.IntegrationEvents;
using Framework.Mediator;
using Framework.Mediator.IntegrationEvents;

namespace CleanArchitecture.Ordering.Messaging.MassTransit.Consumers;

public class OrderStatusChangedEventConsumer(ICommandService commandService, IQueryService queryService) :
    ConsumerBase(commandService, queryService),
    IConsumer<IntegrationEventEnvelope<OrderStatusChangedEvent>>
{
    public Task Consume(ConsumeContext<IntegrationEventEnvelope<OrderStatusChangedEvent>> context)
    {
        var envelope = context.Message;
        var @event = envelope.Payload;
        Console.WriteLine($"{GetType().Name}: Order-Id = {@event.OrderId}");

        var command = new Commands.DoNothings.Command
        {
            Id = @event.OrderId,
        };

        return Handle(command, context.CancellationToken, new RequestExecutionOptions
        {
            CorrelationId = envelope.CorrelationId,
        });
    }
}
