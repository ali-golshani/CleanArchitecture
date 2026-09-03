using IntegrationEventBus.Internal;
using System.Text.Json;

namespace IntegrationEventBus.Serialization;

internal sealed class SystemTextJsonIntegrationEventSerializer(JsonSerializerOptions options)
    : IIntegrationEventSerializer
{
    public string Serialize(object value, Type type)
    {
        return JsonSerializer.Serialize(value, type, options);
    }

    public object Deserialize(string json, Type type)
    {
        return
            JsonSerializer.Deserialize(json, type, options)
            ?? throw new JsonException($"The payload for event type '{type.FullName}' deserialized to null.");
    }
}
