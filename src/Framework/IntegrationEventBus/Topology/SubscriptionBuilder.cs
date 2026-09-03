namespace IntegrationEventBus.Topology;

/// <summary>
/// Configures the event routes and retry policy for a subscription.
/// </summary>
public sealed class SubscriptionBuilder
{
    private readonly Dictionary<Type, Type?> _routes = [];
    private RetryPolicy _retryPolicy = RetryPolicy.Default;

    internal SubscriptionBuilder(string name, string topic)
    {
        Name = name;
        Topic = topic;
    }

    internal string Name { get; }
    internal string Topic { get; }

    /// <summary>
    /// Registers a delivery route without referencing a handler implementation. This is useful in
    /// a producer-only process.
    /// </summary>
    public SubscriptionBuilder Subscribe<TEvent>()
        where TEvent : notnull
    {
        AddRoute(typeof(TEvent), null);
        return this;
    }

    /// <summary>
    /// Registers a delivery route and the handler used by a processor process.
    /// </summary>
    public SubscriptionBuilder Handle<TEvent, THandler>()
        where TEvent : notnull
        where THandler : class, IIntegrationEventHandler<TEvent>
    {
        AddRoute(typeof(TEvent), typeof(THandler));
        return this;
    }

    public SubscriptionBuilder UseRetryPolicy(RetryPolicy retryPolicy)
    {
        ArgumentNullException.ThrowIfNull(retryPolicy);
        retryPolicy.Validate();
        _retryPolicy = retryPolicy;
        return this;
    }

    internal SubscriptionDefinition Build()
    {
        return new(Name, Topic, _retryPolicy, _routes);
    }

    private void AddRoute(Type eventType, Type? handlerType)
    {
        if (_routes.TryGetValue(eventType, out var existingHandler))
        {
            if (existingHandler == handlerType)
            {
                return;
            }

            if (existingHandler is null && handlerType is not null)
            {
                _routes[eventType] = handlerType;
                return;
            }

            throw new InvalidOperationException(
                $"Event type '{eventType.FullName}' is already configured for subscription '{Name}'.");
        }

        _routes.Add(eventType, handlerType);
    }
}
