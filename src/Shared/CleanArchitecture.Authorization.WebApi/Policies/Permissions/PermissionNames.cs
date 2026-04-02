namespace CleanArchitecture.Authorization.WebApi.Policies.Permissions;

internal static class PermissionNames
{
    public const string ClaimType = UserClaimTypes.Permission;

    public static string PermissionName(Permission permission)
    {
        return permission.ToString();
    }
}