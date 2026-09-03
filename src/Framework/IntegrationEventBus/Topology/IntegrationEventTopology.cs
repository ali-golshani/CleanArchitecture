using System.Collections.ObjectModel;

namespace IntegrationEventBus.Topology;

/// <summary>
/// Immutable event and subscription topology shared by publishers and processors.
/// </summary>
public sealed class IntegrationEventTopology
{
    private readonly IReadOnlyDictionary<Type, IntegrationEventDefinition> _eventsByType;
    private readonly IReadOnlyDictionary<string, SubscriptionDefinition> _subscriptionsByName;

    internal IntegrationEventTopology(
        IDictionary<Type, IntegrationEventDefinition> eventsByType,
        IDictionary<string, SubscriptionDefinition> subscriptionsByName)
    {
        _eventsByType = new ReadOnlyDictionary<Type, IntegrationEventDefinition>(
            new Dictionary<Type, IntegrationEventDefinition>(eventsByType));

        _subscriptionsByName = new ReadOnlyDictionary<string, SubscriptionDefinition>(
            new Dictionary<string, SubscriptionDefinition>(subscriptionsByName, StringComparer.OrdinalIgnoreCase));
    }

    public IReadOnlyCollection<IntegrationEventDefinition> Events => [.. _eventsByType.Values];
    public IReadOnlyCollection<SubscriptionDefinition> Subscriptions => [.. _subscriptionsByName.Values];

    public IntegrationEventDefinition GetEvent(Type eventType)
    {
        return
            _eventsByType.TryGetValue(eventType, out var definition)
            ? definition
            : throw new InvalidOperationException($"Integration event type '{eventType.FullName}' is not registered.");
    }

    public IntegrationEventDefinition GetEvent(string eventName)
    {
        foreach (var definition in _eventsByType.Values)
        {
            if (string.Equals(definition.EventName, eventName, StringComparison.OrdinalIgnoreCase))
            {
                return definition;
            }
        }

        throw new InvalidOperationException($"Integration event name '{eventName}' is not registered.");
    }

    public SubscriptionDefinition GetSubscription(string subscriptionName)
    {
        return
            _subscriptionsByName.TryGetValue(subscriptionName, out var definition)
            ? definition
            : throw new InvalidOperationException($"Integration event subscription '{subscriptionName}' is not registered.");
    }

    public IReadOnlyList<SubscriptionDefinition> GetSubscriptions(Type eventType, string topic)
    {
        return [.. _subscriptionsByName.Values.Where(predicate)];

        bool predicate(SubscriptionDefinition subscription)
        {
            return
                string.Equals(subscription.Topic, topic, StringComparison.OrdinalIgnoreCase)
                && subscription.Routes.ContainsKey(eventType);
        }
    }
}
