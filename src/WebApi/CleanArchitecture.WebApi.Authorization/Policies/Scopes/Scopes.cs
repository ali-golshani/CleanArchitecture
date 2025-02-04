namespace CleanArchitecture.WebApi.Authorization.Policies.Scopes;

[Flags]
public enum Scopes : long
{
    Admin = 1,
    BrokerAdmin = 2,
    BrokerClerk = 4,
}
