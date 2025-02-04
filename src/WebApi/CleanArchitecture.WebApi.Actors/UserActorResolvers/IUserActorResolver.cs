using CleanArchitecture.Actors;

namespace CleanArchitecture.WebApi.Actors.UserActorResolvers;

internal interface IUserActorResolver
{
    IEnumerable<Actor> GetActors(ClaimsUser user);


}
