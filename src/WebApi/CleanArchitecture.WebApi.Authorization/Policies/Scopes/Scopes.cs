namespace CleanArchitecture.WebApi.Authorization.Policies.Scopes;

[Flags]
public enum Scopes : long
{
    None = 0,
    Admin = 1,
    BrokerAdmin = 2,
    BrokerClerk = 4,
    All = ~None,
}
