using System.Security.Claims;

namespace CleanArchitecture.Actors;

public static class UserExtensions
{
    public static IEnumerable<Actor> UserActors(this ClaimsPrincipal user)
    {
        var roles = user.FindAll("role").ToList();
        string username = user.Identity?.Name ?? Strings.Question;
        string displayName = user.FindFirst("user_display_name")?.Value ?? Strings.Question;

        var resolvers = new IUserActorResolver[]
        {
            new ProgrammerResolver(),
            new SupervisorActorResolver(),
            new BrokerActorResolver(),
            new CustomerActorResolver(),
        };

        var query = new IUserActorResolver.User(user, roles, username, displayName);
        foreach (var resolver in resolvers)
        {
            foreach (var item in resolver.GetActors(query))
            {
                yield return item;
            }
        }
    }
}
