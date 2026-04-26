namespace CleanArchitecture.Authorization.WebApi.Policies.Roles;

internal static class PolicyNames
{
    private const string PolicyPrefix = "RolePolicy-";

    public static string PolicyName(Claims.Roles roles)
    {
        return $"{PolicyPrefix}{(long)roles}";
    }

    public static bool TryParsePolicyName(string policyName, out Claims.Roles roles)
    {
        if (policyName.StartsWith(PolicyPrefix))
        {
            var rolesString = policyName[PolicyPrefix.Length..];
            if (long.TryParse(rolesString, out var rolesValue))
            {
                roles = (Claims.Roles)rolesValue;
                return true;
            }
        }

        roles = Claims.Roles.None;
        return false;
    }
}