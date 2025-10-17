namespace CleanArchitecture.Actors.WebApi.ActorResolvers;

internal sealed class SupervisorActorResolver : IActorResolver<SupervisorActor>
{
    public SupervisorActor? Resolve(User user)
    {
        string username = user.Username;
        string displayName = user.DisplayName;

        var isSupervisor = user.IsInRole(ClaimTypes.SupervisorRoles);

        if (!isSupervisor)
        {
            return null;
        }

        return new SupervisorActor(username, displayName);
    }
}
