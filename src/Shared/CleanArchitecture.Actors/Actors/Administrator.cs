namespace CleanArchitecture.Actors;

public class Administrator : Actor
{
    public Administrator(string username, string displayName)
        : base(Role.Administrator, username, displayName)
    { }
}
