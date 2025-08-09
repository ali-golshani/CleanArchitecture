namespace CleanArchitecture.Actors.ActorProviders;

public interface IActorProvider
{
    Actor? CurrentActor();
}
