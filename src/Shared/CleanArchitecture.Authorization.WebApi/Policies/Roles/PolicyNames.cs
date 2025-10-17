namespace CleanArchitecture.Authorization.WebApi.Policies.Roles;

internal static class PolicyNames
{
    private const string PolicyPrefix = "RolePolicy-";

    public static string PolicyName(Roles roles)
    {
        return $"{PolicyPrefix}{(long)roles}";
    }

    public static bool TryParsePolicyName(string policyName, out Roles roles)
    {
        if (policyName.StartsWith(PolicyPrefix))
        {
            var rolesString = policyName[PolicyPrefix.Length..];
            if (long.TryParse(rolesString, out var rolesValue))
            {
                roles = (Roles)rolesValue;
                return true;
            }
        }

        roles = Roles.None;
        return false;
    }
}