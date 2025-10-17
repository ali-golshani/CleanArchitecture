using System.Security.Claims;

namespace CleanArchitecture.Actors.WebApi.ActorResolvers;

internal sealed class CustomerActorResolver : IActorResolver<CustomerActor>
{
    public CustomerActor? Resolve(User user)
    {
        string username = user.Username;
        string displayName = user.DisplayName;

        var isCustomer = user.IsInRole(ClaimTypes.CustomerRoles);

        if (!isCustomer)
        {
            return null;
        }

        var customerId = CustomerId(user.Principal);

        if (customerId == null)
        {
            return null;
        }

        return new CustomerActor(customerId.Value, username, displayName);
    }

    private static int? CustomerId(ClaimsPrincipal user)
    {
        if (int.TryParse(user.FindFirst(ClaimTypes.CustomerId)?.Value, out var auctioneerId) && auctioneerId > 0)
        {
            return auctioneerId;
        }
        else
        {
            return null;
        }
    }
}
