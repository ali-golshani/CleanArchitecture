namespace CleanArchitecture.Authorization.WebApi.Policies.Scopes;

internal static class PolicyNames
{
    private const string PolicyPrefix = "ScopePolicy-";

    public static string PolicyName(Scopes scopes)
    {
        return $"{PolicyPrefix}{(long)scopes}";
    }

    public static bool TryParsePolicyName(string policyName, out Scopes scopes)
    {
        if (policyName.StartsWith(PolicyPrefix))
        {
            var scopesString = policyName[PolicyPrefix.Length..];
            if (long.TryParse(scopesString, out var scopesValue))
            {
                scopes = (Scopes)scopesValue;
                return true;
            }
        }

        scopes = default;
        return false;
    }
}