namespace CleanArchitecture.Actors;

public sealed class SupervisorActor(string username, string displayName)
    : Actor(Role.Supervisor, username, displayName);