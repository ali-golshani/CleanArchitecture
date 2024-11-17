namespace Framework.Domain.IntegrationEvents;

public enum IntegrationEventPublishStatus : sbyte
{
    InProcess = 0,
    Published = 1,
    Failed = -1,
}
