using System.Security.Claims;

namespace CleanArchitecture.WebApi.Shared.Actors;

internal record class ClaimsUser(ClaimsPrincipal Principal, List<Claim> Roles, string Username, string DisplayName)
{
    public bool IsInRole(List<string> roles)
    {
        return roles.Exists(x => Roles.Exists(y => y.Value == x));
    }
}
