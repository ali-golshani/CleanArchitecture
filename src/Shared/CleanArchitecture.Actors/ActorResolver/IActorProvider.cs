namespace CleanArchitecture.Actors;

public interface IActorProvider
{
    Actor? CurrentActor();
}
