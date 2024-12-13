namespace CleanArchitecture.Authorization;

public class CustomerPermissionRule<T>(int customerId) : IPermissionRule<T>
{
    public int CustomerId { get; } = customerId;

    public ValueTask<bool> HasPermission(Actor? actor, T content)
    {
        var result =
            actor is CustomerActor customer &&
            customer.CustomerId == CustomerId;

        return ValueTask.FromResult(result);
    }
}
