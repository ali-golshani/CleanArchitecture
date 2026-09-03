namespace IntegrationEventBus.Storage;

internal enum DeliveryStatus : byte
{
    Pending = 0,
    Retrying = 1,
    Succeeded = 2,
    DeadLettered = 3
}
