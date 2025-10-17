using System.Security.Claims;

namespace CleanArchitecture.Actors.WebApi;

internal sealed record class User(ClaimsPrincipal Principal, List<Claim> Roles, string Username, string DisplayName)
{
    public bool IsInRole(List<string> roles)
    {
        return roles.Exists(x => Roles.Exists(y => y.Value == x));
    }
}
