namespace CleanArchitecture.WebApi.Authorization.Policies.Scopes;

[Flags]
public enum Scopes : long
{
    Customers = 1,
    Brokers = 2,
    Orders = 4,
}
