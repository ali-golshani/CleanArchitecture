namespace CleanArchitecture.Authorization.Claims;

[Flags]
public enum Roles : long
{
    None = 0,
    Customer = 1,
    Broker = 2,
    Administrator = 4,
    Programmer = 8,
}
