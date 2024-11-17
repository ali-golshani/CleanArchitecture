using CleanArchitecture.Actors;
using CleanArchitecture.WebApi.Shared.Actors.UserActorResolvers;
using System.Security.Claims;

namespace CleanArchitecture.WebApi.Shared.Actors;

internal static class ClaimsActorResolver
{
    public static IEnumerable<Actor> Actors(ClaimsPrincipal user)
    {
        var query = ClaimsUser(user);

        if (query is null)
        {
            yield break;
        }

        var resolvers = new IUserActorResolver[]
        {
            new ProgrammerResolver(),
            new SupervisorActorResolver(),
            new BrokerActorResolver(),
            new CustomerActorResolver(),
        };

        foreach (var resolver in resolvers)
        {
            foreach (var item in resolver.GetActors(query))
            {
                yield return item;
            }
        }
    }

    private static ClaimsUser? ClaimsUser(ClaimsPrincipal user)
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
                ?.Value ?? Strings.Question;
        }

        string displayName =
            user
            .FindFirst(x => MatchClaimType(x, ClaimTypes.DisplayName))
            ?.Value ?? Strings.Question;

        return new ClaimsUser(user, roles, username, displayName);
    }

    private static bool MatchClaimType(Claim x, string claimType)
    {
        return x.Type.Equals(claimType, StringComparison.OrdinalIgnoreCase);
    }
}
