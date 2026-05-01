namespace CleanArchitecture.Actors;

public sealed class InternalServiceActor(string service, string? displayName = null)
    : Actor(Role.InternalService, service, displayName ?? service)
{
    public override string ToString()
    {
        return $"[Internal Service] . [{DisplayName}]";
    }
}
