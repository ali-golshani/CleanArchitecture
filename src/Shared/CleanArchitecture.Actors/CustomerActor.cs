namespace CleanArchitecture.Actors;

public sealed class CustomerActor(int customerId, string username, string displayName)
    : Actor(Role.Customer, username, displayName)
{
    public int CustomerId { get; } = customerId;

    public override string ToString()
    {
        return $"[Customer {CustomerId}] . [{Username}] . [{DisplayName}]";
    }
}
