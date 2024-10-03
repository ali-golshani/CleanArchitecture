namespace CleanArchitecture.Actors;

public class HttpActorPriority
{
    public Type? ActorType { get; private set; }

    public void SetMostPriorityActorType(Type? actorType)
    {
        ActorType = actorType;
    }
}
