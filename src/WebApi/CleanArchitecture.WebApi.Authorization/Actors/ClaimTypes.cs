namespace CleanArchitecture.WebApi.Authorization.Actors;

public static class ClaimTypes
{
    public const string Role = "role";
    public const string Username = "name";
    public const string DisplayName = "displayName";

    public const string BrokerId = "brokerId";
    public const string CustomerId = "customerId";

    public static readonly List<string> ProgrammerRoles =
    [
        "developer",
        "programmer",
    ];

    public static readonly List<string> SupervisorRoles =
    [
        "overseer",
        "supervisor",
    ];

    public static readonly List<string> BrokerRoles =
    [
        "broker.clerk",
        "broker.trader",
        "broker.manager"
    ];

    public static readonly List<string> CustomerRoles =
    [
        "customer"
    ];
}
