namespace CleanArchitecture.Actors;

internal class HttpActorProvider : IActorProvider
{
    private readonly IClaimsPrincipalResolver? claimsPrincipalResolver;
    private readonly HttpActorPriority actorPriority;

    public HttpActorProvider(IClaimsPrincipalResolver? claimsPrincipalResolver, HttpActorPriority actorPriority)
    {
        this.claimsPrincipalResolver = claimsPrincipalResolver;
        this.actorPriority = actorPriority;
    }

    public Actor? CurrentActor()
    {
        var user = claimsPrincipalResolver?.User;

        if (user is null)
        {
            return null;
        }

        var actors = user.UserActors().ToList();

        if (actors.Count == 0)
        {
            return null;
        }

        if (actorPriority.ActorType != null)
        {
            return
                actors.Find(actorPriority.ActorType.IsInstanceOfType)
                ?? actors[0];
        }
        else
        {
            return actors[0];
        }
    }
}
