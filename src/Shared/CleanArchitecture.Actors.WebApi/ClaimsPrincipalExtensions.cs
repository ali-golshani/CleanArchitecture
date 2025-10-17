using System.Security.Claims;

namespace CleanArchitecture.Actors.WebApi;

internal static class ClaimsPrincipalExtensions
{
    private const string Question = "?";

    public static User? GetUser(this ClaimsPrincipal user)
    {
        if (user?.Identity?.IsAuthenticated != true)
        {
            return null;
        }

        var roles =
            user
            .FindAll(x => MatchClaimType(x, ClaimTypes.Role))
            .ToList();

        string? username = user.Identity?.Name;

        if (string.IsNullOrEmpty(username))
        {
            username =
                user
                .FindFirst(x => MatchClaimType(x, ClaimTypes.Username))
                ?.Value ?? Question;
        }

        string displayName =
            user
            .FindFirst(x => MatchClaimType(x, ClaimTypes.DisplayName))
            ?.Value ?? Question;

        return new User(user, roles, username, displayName);
    }

    private static bool MatchClaimType(Claim x, string claimType)
    {
        return x.Type.Equals(claimType, StringComparison.OrdinalIgnoreCase);
    }
}
