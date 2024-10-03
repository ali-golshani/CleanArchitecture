using System.Security.Claims;

namespace CleanArchitecture.Actors;

internal sealed class CustomerActorResolver : IUserActorResolver
{
    public IEnumerable<Actor> GetActors(IUserActorResolver.User user)
    {
        var roles = user.Roles;
        string username = user.Username;
        string displayName = user.DisplayName;

        if (roles.Exists(x => x.Value == "auctioneer"))
        {
            var customerId = CustomerId(user.Principal);
            if (customerId != null)
            {
                yield return new CustomerActor(customerId.Value, username, displayName);
            }
        }
    }

    private static int? CustomerId(ClaimsPrincipal user)
    {
        if (int.TryParse(user.FindFirst("user_spot_auctioneer_id")?.Value, out var auctioneerId) && auctioneerId > 0)
        {
            return auctioneerId;
        }
        else
        {
            return null;
        }
    }
}
