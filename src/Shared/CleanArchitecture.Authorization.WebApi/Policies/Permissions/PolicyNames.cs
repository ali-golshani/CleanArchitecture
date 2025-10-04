namespace CleanArchitecture.Authorization.WebApi.Policies.Permissions;

internal static class PolicyNames
{
    private const string PolicyPrefix = "PermissionPolicy-";

    public static string PolicyName(Permission permission)
    {
        return $"{PolicyPrefix}{(int)permission}";
    }

    public static bool TryParsePolicyName(string policyName, out Permission permission)
    {
        if (policyName.StartsWith(PolicyPrefix))
        {
            var permissionString = policyName[PolicyPrefix.Length..];
            if (Enum.TryParse(permissionString, out permission))
            {
                return true;
            }
        }

        permission = default;
        return false;
    }
}