using CleanArchitecture.Actors;

namespace CleanArchitecture.WebApi.Shared.Actors.UserActorResolvers;

internal sealed class ProgrammerResolver : IUserActorResolver
{
    public IEnumerable<Actor> GetActors(ClaimsUser user)
    {
        string username = user.Username;
        string displayName = user.DisplayName;

        var isProgrammer = user.IsInRole(ClaimTypes.ProgrammerRoles);

        if (isProgrammer)
        {
            yield return new Programmer(username, displayName);
        }

        var isSupervisor = user.IsInRole(ClaimTypes.SupervisorRoles);

        if (isSupervisor)
        {
            yield return new SupervisorActor(username, displayName);
        }
    }
}
