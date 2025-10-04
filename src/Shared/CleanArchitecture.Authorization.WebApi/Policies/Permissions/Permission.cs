namespace CleanArchitecture.Authorization.WebApi.Policies.Permissions;

public enum Permission
{
    None = 0,
    ReadOrders = 1,
    RegisterOrder = 2,
    CancelOrder = 3,
    RejectOrder = 4,
}
