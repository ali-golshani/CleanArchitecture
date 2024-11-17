using CleanArchitecture.Actors;

namespace CleanArchitecture.WebApi.Shared.Actors.UserActorResolvers;

internal interface IUserActorResolver
{
    IEnumerable<Actor> GetActors(ClaimsUser user);


}
