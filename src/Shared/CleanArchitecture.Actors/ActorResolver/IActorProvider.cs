namespace CleanArchitecture.Actors;

internal interface IActorProvider
{
    Actor? CurrentActor();
}
