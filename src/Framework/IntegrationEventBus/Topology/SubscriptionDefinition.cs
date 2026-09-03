using System.Collections.ObjectModel;

namespace IntegrationEventBus.Topology;

/// <summary>
/// Describes one independent consumer of a topic.
/// </summary>
public sealed class SubscriptionDefinition
{
    private readonly IReadOnlyDictionary<Type, Type?> _routes;

    internal SubscriptionDefinition(
        string name,
        string topic,
        RetryPolicy retryPolicy,
        IDictionary<Type, Type?> routes)
    {
        Name = name;
        Topic = topic;
        RetryPolicy = retryPolicy;
        _routes = new ReadOnlyDictionary<Type, Type?>(new Dictionary<Type, Type?>(routes));
    }

    public string Name { get; }
    public string Topic { get; }
    public RetryPolicy RetryPolicy { get; }

    /// <summary>
    /// Gets subscribed CLR event types and their optional handler types. A producer can register a
    /// subscription route without referencing the consumer's handler assembly.
    /// </summary>
    public IReadOnlyDictionary<Type, Type?> Routes => _routes;

    public bool HasHandlers => _routes.Values.Any(static handlerType => handlerType is not null);
}
