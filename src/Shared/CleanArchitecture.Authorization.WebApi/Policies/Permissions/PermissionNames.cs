namespace CleanArchitecture.Authorization.WebApi.Policies.Permissions;

internal static class PermissionNames
{
    public const string ClaimType = "permission";

    public static string PermissionName(Permission permission)
    {
        return permission.ToString();
    }
}