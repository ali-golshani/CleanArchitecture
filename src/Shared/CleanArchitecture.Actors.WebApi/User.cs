using System.Security.Claims;

namespace CleanArchitecture.Actors.WebApi;

internal sealed record class User(ClaimsPrincipal Principal, List<Claim> Roles, string Username, string DisplayName)
{
    public bool IsInRole(IReadOnlyCollection<string> roles)
    {
        return roles.Any(x => Roles.Exists(y => y.Value.Equals(x, StringComparison.OrdinalIgnoreCase)));
    }
}
