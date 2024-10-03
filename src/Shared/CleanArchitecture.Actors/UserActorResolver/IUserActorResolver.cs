using System.Security.Claims;

namespace CleanArchitecture.Actors;

internal interface IUserActorResolver
{
    public record class User(ClaimsPrincipal Principal, List<Claim> Roles, string Username, string DisplayName);

    IEnumerable<Actor> GetActors(User user);
}
