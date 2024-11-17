namespace CleanArchitecture.WebApi.Shared.Authorization.Scopes;

[Flags]
public enum Scopes : long
{
    Admin = 1,
    BrokerAdmin = 2,
    BrokerClerk = 4,
}
