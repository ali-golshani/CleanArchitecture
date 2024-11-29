namespace CleanArchitecture.Actors;

public class BrokerActor : Actor
{
    public BrokerActor(int brokerId, string username, string displayName, bool? isClerk)
        : base(Role.Broker, username, displayName)
    {
        BrokerId = brokerId;
        IsClerk = isClerk;
    }

    public int BrokerId { get; }
    public bool? IsClerk { get; }

    public override string ToString()
    {
        return $"Broker {BrokerId} ⚊ {Username} ⚊ {DisplayName}";
    }
}
