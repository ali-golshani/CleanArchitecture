namespace CleanArchitecture.Actors.ActorProviders;

internal sealed class FixedActorProvider : IActorProvider
{
    private Actor? actor = null;

    public Actor? CurrentActor()
    {
        return actor;
    }

    public void SetActor(Actor actor)
    {
        this.actor = actor;
    }
}
