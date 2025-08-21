namespace CleanArchitecture.WebApi.Authorization.Policies.Scopes;

[Flags]
public enum Scopes : long
{
    None = 0,
    Customers = 1,
    Brokers = 2,
    Orders = 4,
    All = ~None,
}
