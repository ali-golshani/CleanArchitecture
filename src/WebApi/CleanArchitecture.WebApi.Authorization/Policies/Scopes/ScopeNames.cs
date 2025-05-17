namespace CleanArchitecture.WebApi.Authorization.Policies.Scopes;

internal static class ScopeNames
{
    public const string ClaimType = "scope";

    public static string ScopeName(Scopes scopes)
    {
        return scopes.ToString();
    }
}
