namespace CleanArchitecture.Actors;

public class CustomerActor : Actor
{
    public CustomerActor(int customerId, string username, string displayName)
        : base(Role.Customer, username, displayName)
    {
        CustomerId = customerId;
    }

    public int CustomerId { get; }

    public override string ToString()
    {
        return $"[Customer {CustomerId}] . [{Username}] . [{DisplayName}]";
    }
}
