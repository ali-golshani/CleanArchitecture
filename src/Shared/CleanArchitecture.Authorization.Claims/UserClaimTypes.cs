namespace CleanArchitecture.Authorization.Claims;

public static class UserClaimTypes
{
    public const string Role = System.Security.Claims.ClaimTypes.Role;
    public const string Username = System.Security.Claims.ClaimTypes.Name;
    public const string UserId = System.Security.Claims.ClaimTypes.NameIdentifier;
    public const string DisplayName = "displayname";
    public const string Permission = "permission";

    public const string BrokerId = "brokerid";
    public const string CustomerId = "customerid";

    public static readonly List<string> ProgrammerRoles =
    [
        "developer",
        "programmer",
    ];

    public static readonly List<string> AdministratorRoles =
    [
        "admin",
        "administrator",
    ];

    public static readonly List<string> SupervisorRoles =
    [
        "overseer",
        "supervisor",
    ];

    public static readonly List<string> BrokerRoles =
    [
        "broker",
    ];

    public static readonly List<string> CustomerRoles =
    [
        "customer"
    ];
}
