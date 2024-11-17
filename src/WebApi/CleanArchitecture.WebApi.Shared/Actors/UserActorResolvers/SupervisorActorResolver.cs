using CleanArchitecture.Actors;

namespace CleanArchitecture.WebApi.Shared.Actors.UserActorResolvers;

internal sealed class SupervisorActorResolver : IUserActorResolver
{
    public IEnumerable<Actor> GetActors(ClaimsUser user)
    {
        string username = user.Username;
        string displayName = user.DisplayName;

        var isSupervisor = user.IsInRole(ClaimTypes.SupervisorRoles);

        if (isSupervisor)
        {
            yield return new SupervisorActor(username, displayName);
        }
    }
}
