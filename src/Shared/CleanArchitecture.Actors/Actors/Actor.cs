namespace CleanArchitecture.Actors;

public abstract class Actor
{
    protected Actor(Role role, string username, string displayName)
    {
        Role = role;
        Username = username;
        DisplayName = displayName;
    }

    public Role Role { get; }
    public string Username { get; }
    public string DisplayName { get; }

    public override string ToString()
    {
        return $"[{Role}] . [{Username}] . [{DisplayName}]";
    }
}
