namespace CleanArchitecture.Actors.WebApi.ActorResolvers;

internal interface IUserActorsResolver
{
    IEnumerable<Actor> GetActors(User user);
}
