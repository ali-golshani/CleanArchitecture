namespace CleanArchitecture.Actors;

public sealed class Programmer(string username, string displayName)
    : Actor(Role.Programmer, username, displayName);