namespace CleanArchitecture.Actors.WebApi.ActorResolvers;

internal sealed class SupervisorActorResolver : IUserActorsResolver
{
    public IEnumerable<Actor> GetActors(User user)
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
