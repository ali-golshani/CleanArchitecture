namespace CleanArchitecture.Actors;

internal sealed class SupervisorActorResolver : IUserActorResolver
{
    public IEnumerable<Actor> GetActors(IUserActorResolver.User user)
    {
        var roles = user.Roles;
        string username = user.Username;
        string displayName = user.DisplayName;

        if (roles.Exists(x => x.Value == "ime.overseer"))
        {
            yield return new SupervisorActor(username, displayName);
        }
    }
}
