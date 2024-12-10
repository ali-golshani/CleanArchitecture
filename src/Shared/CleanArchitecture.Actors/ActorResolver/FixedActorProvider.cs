namespace CleanArchitecture.Actors;

internal class FixedActorProvider : IActorProvider
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
