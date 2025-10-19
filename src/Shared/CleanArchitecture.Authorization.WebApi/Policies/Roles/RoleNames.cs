namespace CleanArchitecture.Authorization.WebApi.Policies.Roles;

internal static class RoleNames
{
    public const string ClaimType = "Role";

    public static string RoleName(Roles roles)
    {
        return roles.ToString().ToLower();
    }
}
