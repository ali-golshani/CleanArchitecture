namespace IntegrationEventBus.Core.Topology;

/// <summary>
/// Maps a CLR event type to its stable contract name and topic.
/// </summary>
public sealed record IntegrationEventDefinition(Type EventType, string EventName, string Topic);
