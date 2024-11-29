namespace CleanArchitecture.Actors;

public class InternalServiceActor : Actor
{
    public InternalServiceActor(string service, string? displayName = null)
        : base(Role.InternalService, service, displayName ?? service)
    { }

    public override string ToString()
    {
        return $"[Internal Service] . [{DisplayName}]";
    }
}
