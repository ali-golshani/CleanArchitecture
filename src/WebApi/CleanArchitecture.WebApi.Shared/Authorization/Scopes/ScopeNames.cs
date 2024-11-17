namespace CleanArchitecture.WebApi.Shared.Authorization.Scopes;

internal static class ScopeNames
{
    public static string ScopeName(Scopes scopes)
    {
        return scopes.ToString();
    }
}