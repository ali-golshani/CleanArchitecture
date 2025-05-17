namespace CleanArchitecture.WebApi.Authorization.Policies.Permissions;

internal static class PermissionNames
{
    public const string ClaimType = "permissions";

    public static string PermissionName(Permission permission)
    {
        return permission.ToString();
    }
}