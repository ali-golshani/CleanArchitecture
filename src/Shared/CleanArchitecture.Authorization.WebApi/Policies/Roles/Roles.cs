namespace CleanArchitecture.Authorization.WebApi.Policies.Roles;

[Flags]
public enum Roles : long
{
    None = 0,
    Customer = 1,
    Broker = 2,
    Admin = 4,
    Programmer = 8,
}
