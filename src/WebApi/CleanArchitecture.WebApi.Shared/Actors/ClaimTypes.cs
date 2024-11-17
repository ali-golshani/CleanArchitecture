namespace CleanArchitecture.WebApi.Shared.Actors;

public static class ClaimTypes
{
    public const string Role = "role";
    public const string Username = "name";
    public const string DisplayName = "user_display_name";

    public const string BrokerId = "brokerId";
    public const string CustomerId = "customerId";

    public static readonly List<string> ProgrammerRoles =
    [
        "developer",
        "programmer",
    ];

    public static readonly List<string> SupervisorRoles =
    [
        "ime.admin",
        "ime.overseer",
    ];

    public static readonly List<string> BrokerRoles =
    [
        "broker.clerk",
        "broker.trader",
        "broker.spot.trader.manager"
    ];

    public static readonly List<string> CustomerRoles =
    [
        "customer"
    ];
}
