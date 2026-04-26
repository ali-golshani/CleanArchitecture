namespace CleanArchitecture.Actors;

public class BrokerActor : Actor
{
    public BrokerActor(int brokerId, string username, string displayName)
        : base(Role.Broker, username, displayName)
    {
        BrokerId = brokerId;
    }

    public int BrokerId { get; }

    public override string ToString()
    {
        return $"[Broker {BrokerId}] . [{Username}] . [{DisplayName}]";
    }
}
