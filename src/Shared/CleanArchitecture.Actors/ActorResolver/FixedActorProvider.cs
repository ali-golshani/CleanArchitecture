namespace CleanArchitecture.Actors;

internal class FixedActorProvider : IActorProvider
{
    private Actor? user = null;

    public Actor? CurrentActor()
    {
        return user;
    }

    public void SetUser(Actor user)
    {
        this.user = user;
    }
}
