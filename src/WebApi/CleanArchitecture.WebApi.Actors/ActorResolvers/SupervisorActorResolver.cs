using CleanArchitecture.Actors;

namespace CleanArchitecture.WebApi.Actors.ActorResolvers;

internal sealed class SupervisorActorResolver : ActorResolverBase
{
    public override IEnumerable<Actor> GetActors(User user)
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
