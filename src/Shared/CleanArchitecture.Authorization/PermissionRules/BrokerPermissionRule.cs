namespace CleanArchitecture.Authorization;

public class BrokerPermissionRule<T>(int brokerId) : IPermissionRule<T>
{
    public int BrokerId { get; } = brokerId;

    public ValueTask<bool> HasPermission(Actor? actor, T content)
    {
        var result =
            actor is BrokerActor broker &&
            broker.BrokerId == BrokerId;

        return ValueTask.FromResult(result);
    }
}
