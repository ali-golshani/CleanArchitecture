﻿using CleanArchitecture.Actors;
using System.Security.Claims;

namespace CleanArchitecture.WebApi.Shared.Actors.UserActorResolvers;

internal sealed class CustomerActorResolver : IUserActorResolver
{
    public IEnumerable<Actor> GetActors(ClaimsUser user)
    {
        string username = user.Username;
        string displayName = user.DisplayName;

        var isCustomer = user.IsInRole(ClaimTypes.CustomerRoles);

        if (isCustomer)
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
