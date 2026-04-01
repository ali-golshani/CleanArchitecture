namespace CleanArchitecture.Actors.WebApi.ActorResolvers;

internal sealed class ProgrammerResolver : IActorResolver<Programmer>
{
    public Programmer? Resolve(User user)
    {
        string username = user.Username;
        string displayName = user.DisplayName;

        var isProgrammer = user.IsInRole(UserClaimTypes.ProgrammerRoles);

        if (!isProgrammer)
        {
            return null;
        }

        return new Programmer(username, displayName);
    }
}
