namespace IntegrationEventBus.Topology;

/// <summary>
/// Builds the explicit event and subscription topology. No assembly scanning is performed.
/// </summary>
public sealed class IntegrationEventTopologyBuilder
{
    private readonly Dictionary<Type, IntegrationEventDefinition> _events = [];
    private readonly Dictionary<string, SubscriptionBuilder> _subscriptions = new(StringComparer.OrdinalIgnoreCase);

    public IntegrationEventTopologyBuilder Event<TEvent>(string eventName, string topic)
        where TEvent : notnull
    {
        ValidateName(eventName, nameof(eventName));
        ValidateName(topic, nameof(topic));

        var eventType = typeof(TEvent);
        if (_events.ContainsKey(eventType))
        {
            throw new InvalidOperationException(
                $"Integration event type '{eventType.FullName}' is already registered.");
        }

        if (_events.Values.Any(definition =>
                string.Equals(definition.EventName, eventName, StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException(
                $"Integration event name '{eventName}' is already registered.");
        }

        _events.Add(eventType, new IntegrationEventDefinition(eventType, eventName, topic));
        return this;
    }

    public IntegrationEventTopologyBuilder Subscription(
        string name,
        string topic,
        Action<SubscriptionBuilder> configure)
    {
        ValidateName(name, nameof(name));
        ValidateName(topic, nameof(topic));
        ArgumentNullException.ThrowIfNull(configure);

        if (_subscriptions.ContainsKey(name))
        {
            throw new InvalidOperationException(
                $"Integration event subscription '{name}' is already registered.");
        }

        var builder = new SubscriptionBuilder(name, topic);
        configure(builder);
        _subscriptions.Add(name, builder);
        return this;
    }

    internal IntegrationEventTopology Build()
    {
        var subscriptions = _subscriptions.Values
            .Select(static builder => builder.Build())
            .ToDictionary(static definition => definition.Name, StringComparer.OrdinalIgnoreCase);

        foreach (var subscription in subscriptions.Values)
        {
            foreach (var eventType in subscription.Routes.Keys)
            {
                if (!_events.TryGetValue(eventType, out var eventDefinition))
                {
                    throw new InvalidOperationException(
                        $"Event type '{eventType.FullName}' used by subscription '{subscription.Name}' " +
                        "has not been registered.");
                }

                if (!string.Equals(
                        eventDefinition.Topic,
                        subscription.Topic,
                        StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException(
                        $"Event '{eventDefinition.EventName}' belongs to topic '{eventDefinition.Topic}', " +
                        $"but subscription '{subscription.Name}' belongs to topic '{subscription.Topic}'.");
                }
            }
        }

        return new IntegrationEventTopology(_events, subscriptions);
    }

    private static void ValidateName(string value, string parameterName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, parameterName);
        if (value.Length > 200)
        {
            throw new ArgumentException("Names cannot be longer than 200 characters.", parameterName);
        }
    }
}
