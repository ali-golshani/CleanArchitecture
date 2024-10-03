using System.Security.Claims;

namespace CleanArchitecture.Actors;

internal sealed class ProgrammerResolver : IUserActorResolver
{
    public IEnumerable<Actor> GetActors(IUserActorResolver.User user)
    {
        var roles = user.Roles;
        string username = user.Username;
        string displayName = user.DisplayName;

        if (roles.Exists(x => x.Value == "developer") ||
            roles.Exists(x => x.Value == "programmer"))
        {
            yield return new Programmer(username, displayName);
        }

        if (roles.Exists(x => x.Value == "ime.admin"))
        {
            yield return new Administrator(username, displayName);
            yield return new BrokerActor(2, username, displayName, true);
            yield return new CustomerActor(CustomerId(user.Principal) ?? 11, username, displayName);
            yield return new SupervisorActor(username, displayName);
        }
    }

    private static int? CustomerId(ClaimsPrincipal user)
    {
        if (int.TryParse(user.FindFirst("user_spot_auctioneer_id")?.Value, out var customerId) && customerId > 0)
        {
            return customerId;
        }

        return null;
    }
}
