namespace CleanArchitecture.Actors;

public class SupervisorActor : Actor
{
    public SupervisorActor(string username, string displayName)
        : base(Role.Supervisor, username, displayName)
    { }
}
