namespace CleanArchitecture.Actors;

public sealed class Administrator(string username, string displayName)
    : Actor(Role.Administrator, username, displayName);
