using CleanArchitecture.Authorization.Claims;

namespace CleanArchitecture.Actors.WebApi.ActorResolvers;

internal sealed class AdministratorResolver : IActorResolver<Administrator>
{
    public Administrator? Resolve(User user)
    {
        string username = user.Username;
        string displayName = user.DisplayName;

        var isAdmin = user.IsInRole(UserClaimTypes.AdministratorRoles);

        if (!isAdmin)
        {
            return null;
        }

        return new Administrator(username, displayName);
    }
}
