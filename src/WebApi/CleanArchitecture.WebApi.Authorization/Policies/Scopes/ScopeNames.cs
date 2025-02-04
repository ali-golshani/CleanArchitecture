namespace CleanArchitecture.WebApi.Authorization.Policies.Scopes;

internal static class ScopeNames
{
    public static string ScopeName(Scopes scopes)
    {
        return scopes.ToString();
    }
}