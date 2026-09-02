using CleanArchitecture.Ordering.IntegrationEvents;
using IntegrationEventBus.Core.Topology;

namespace CleanArchitecture.Ordering.Messaging.SqlEventBus.Subscribers;

public static class ServicesConfiguration
{
    public static void ConfigureTopology(IntegrationEventTopologyBuilder topology)
    {
        topology.Event<OrderStatusChangedEvent>(
            nameof(OrderStatusChangedEvent),
            OrderStatusChangedEvent.EventTopic);

        topology.Subscription(
            "Group-A",
            OrderStatusChangedEvent.EventTopic,
            subscription => subscription.Handle<OrderStatusChangedEvent, OrderStatusChangedEventSubscriberA>());

        topology.Subscription(
            "Group-B",
            OrderStatusChangedEvent.EventTopic,
            subscription => subscription.Handle<OrderStatusChangedEvent, OrderStatusChangedEventSubscriberB>());
    }
}
