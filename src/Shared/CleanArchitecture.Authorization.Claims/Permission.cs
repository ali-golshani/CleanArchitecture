namespace CleanArchitecture.Authorization.Claims;

public enum Permission
{
    None = 0,
    ReadOrders = 1,
    RegisterOrder = 2,
    CancelOrder = 3,
    RejectOrder = 4,
    UserManagement = 5,
}
