namespace CleanArchitecture.WebApi.Authorization.Policies.Permissions;

internal static class PermissionNames
{
    public static string PermissionName(Permission permission)
    {
        return permission.ToString();
    }
}