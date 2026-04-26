namespace CleanArchitecture.Actors;

public class Programmer : Actor
{
    public Programmer(string username, string displayName) 
        : base(Role.Programmer, username, displayName)
    { }
}
