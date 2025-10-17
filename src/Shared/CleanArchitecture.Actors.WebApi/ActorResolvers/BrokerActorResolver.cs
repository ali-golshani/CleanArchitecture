using System.Security.Claims;

namespace CleanArchitecture.Actors.WebApi.ActorResolvers;

internal sealed class BrokerActorResolver : IActorResolver<BrokerActor>
{
    public BrokerActor? Resolve(User user)
    {
        string username = user.Username;
        string displayName = user.DisplayName;

        var brokerId = BrokerId(user.Principal);

        if (brokerId is null)
        {
            return null;
        }

        var isBroker = user.IsInRole(ClaimTypes.BrokerRoles);

        if (!isBroker)
        {
            return null;
        }

        return new BrokerActor(brokerId.Value, username, displayName, true);
    }

    private static int? BrokerId(ClaimsPrincipal user)
    {
        if (int.TryParse(user.FindFirst(ClaimTypes.BrokerId)?.Value, out var brokerId) && brokerId > 0)
        {
            return brokerId;
        }
        else
        {
            return null;
        }
    }
}
