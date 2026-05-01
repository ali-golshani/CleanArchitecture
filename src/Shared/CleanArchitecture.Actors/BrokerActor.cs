namespace CleanArchitecture.Actors;

public sealed class BrokerActor(int brokerId, string username, string displayName)
    : Actor(Role.Broker, username, displayName)
{
    public int BrokerId { get; } = brokerId;

    public override string ToString()
    {
        return $"[Broker {BrokerId}] . [{Username}] . [{DisplayName}]";
    }
}
