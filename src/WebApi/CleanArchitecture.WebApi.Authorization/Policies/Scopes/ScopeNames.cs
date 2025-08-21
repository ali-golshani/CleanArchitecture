namespace CleanArchitecture.WebApi.Authorization.Policies.Scopes;

internal static class ScopeNames
{
    public const string ClaimType = "functional-scope";

    public static string ScopeName(Scopes scopes)
    {
        return scopes.ToString();
    }
}
