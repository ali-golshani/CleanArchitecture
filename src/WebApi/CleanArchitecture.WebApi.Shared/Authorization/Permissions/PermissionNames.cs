namespace CleanArchitecture.WebApi.Shared.Authorization.Permissions;

internal static class PermissionNames
{
    public static string PermissionName(Permission permission)
    {
        return permission.ToString();
    }
}