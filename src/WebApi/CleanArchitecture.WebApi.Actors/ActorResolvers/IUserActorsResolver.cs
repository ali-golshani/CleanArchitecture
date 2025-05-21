using CleanArchitecture.Actors;

namespace CleanArchitecture.WebApi.Actors.ActorResolvers;

internal interface IUserActorsResolver
{
    IEnumerable<Actor> GetActors(User user);
}
