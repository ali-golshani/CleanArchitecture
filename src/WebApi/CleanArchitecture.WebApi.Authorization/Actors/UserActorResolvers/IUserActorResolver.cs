using CleanArchitecture.Actors;

namespace CleanArchitecture.WebApi.Authorization.Actors.UserActorResolvers;

internal interface IUserActorResolver
{
    IEnumerable<Actor> GetActors(ClaimsUser user);


}
