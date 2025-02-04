using CleanArchitecture.Actors;

namespace CleanArchitecture.WebApi.Actors.ActorResolvers;

internal abstract class ActorResolverBase
{
    public abstract IEnumerable<Actor> GetActors(User user);
}
