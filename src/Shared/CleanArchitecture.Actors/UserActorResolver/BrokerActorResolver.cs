using System.Security.Claims;

namespace CleanArchitecture.Actors;

internal sealed class BrokerActorResolver : IUserActorResolver
{
    public IEnumerable<Actor> GetActors(IUserActorResolver.User user)
    {
        var roles = user.Roles;
        string username = user.Username;
        string displayName = user.DisplayName;

        var brokerId = BrokerId(user.Principal);

        if (brokerId is null)
        {
            yield break;
        }

        var isBroker =
            roles.Exists(x => x.Value == "broker.clerk") ||
            roles.Exists(x => x.Value == "broker.trader") ||
            roles.Exists(x => x.Value == "broker.spot.trader.manager");

        if (isBroker)
        {
            yield return new BrokerActor(brokerId.Value, username, displayName, true);
        }
    }

    private static int? BrokerId(ClaimsPrincipal user)
    {
        if (int.TryParse(user.FindFirst("tenant_spot_id")?.Value, out var brokerId) && brokerId > 0)
        {
            return brokerId;
        }
        else
        {
            return null;
        }
    }
}
