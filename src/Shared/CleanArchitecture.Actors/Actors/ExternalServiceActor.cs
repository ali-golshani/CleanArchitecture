namespace CleanArchitecture.Actors;

public class ExternalServiceActor : Actor
{
    public ExternalServiceActor(string service, string serviceName)
        : base(Role.ExternalService, service, serviceName)
    { }
}
