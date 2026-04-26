using CleanArchitecture.Authorization.Claims;

namespace CleanArchitecture.Authorization.WebApi.Policies.Roles;

internal static class RoleNames
{
    public const string ClaimType = UserClaimTypes.Role;

    public static string RoleName(Claims.Roles roles)
    {
        return roles.ToString().ToLower();
    }
}
