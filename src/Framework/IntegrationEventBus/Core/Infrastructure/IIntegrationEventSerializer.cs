namespace IntegrationEventBus.Core.Infrastructure;

public interface IIntegrationEventSerializer
{
    string Serialize(object value, Type type);
    object Deserialize(string json, Type type);
}
