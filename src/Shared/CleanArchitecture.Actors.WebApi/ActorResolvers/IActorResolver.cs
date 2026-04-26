namespace CleanArchitecture.Actors.WebApi.ActorResolvers;

internal interface IActorResolver<out TActor> where TActor : Actor
{
    TActor? Resolve(User user);
}
