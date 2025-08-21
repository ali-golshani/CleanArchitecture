namespace CleanArchitecture.WebApi.Authorization.Policies.Permissions;

public enum Permission
{
    None = 0,
    ReadOrders = 1,
    PlaceOrder = 2,
    CancelOrder = 3,
    RejectOrder = 4,
}
