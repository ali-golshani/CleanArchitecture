using CleanArchitecture.Actors;

namespace CleanArchitecture.WebApi.Shared.Actors;

internal class HttpActorProvider : IActorProvider
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public HttpActorProvider(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    public Actor? CurrentActor()
    {
        var user = httpContextAccessor?.HttpContext?.User;

        if (user is null)
        {
            return null;
        }

        var actors = ClaimsActorResolver.Actors(user).ToList();

        if (actors.Count == 0)
        {
            return null;
        }

        return actors[0];
    }
}
