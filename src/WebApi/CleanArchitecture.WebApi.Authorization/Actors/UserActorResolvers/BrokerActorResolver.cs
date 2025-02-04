using CleanArchitecture.Actors;
using System.Security.Claims;

namespace CleanArchitecture.WebApi.Authorization.Actors.UserActorResolvers;

internal sealed class BrokerActorResolver : IUserActorResolver
{
    public IEnumerable<Actor> GetActors(ClaimsUser user)
    {
        string username = user.Username;
        string displayName = user.DisplayName;

        var brokerId = BrokerId(user.Principal);

        if (brokerId is null)
        {
            yield break;
        }

        var isBroker = user.IsInRole(ClaimTypes.BrokerRoles);

        if (isBroker)
        {
            yield return new BrokerActor(brokerId.Value, username, displayName, true);
        }
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
