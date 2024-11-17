using Microsoft.AspNetCore.Authorization;

namespace CleanArchitecture.WebApi.Shared.Authorization.Permissions;

internal static class Configuration
{
    public const string ClaimType = "permissions";

    public static string PolicyName(Permission permission)
    {
        return permission.ToString();
    }

    public static string PermissionName(Permission permission)
    {
        return PermissionNames.PermissionName(permission);
    }

    public static void RegisterPermissionsPolicies(AuthorizationOptions options)
    {
        foreach (var permission in Enum.GetValues<Permission>())
        {
            options.AddPolicy(PolicyName(permission), policy =>
            {
                policy.Requirements.Add(new PermissionRequirement(permission));
            });
        }
    }
}
