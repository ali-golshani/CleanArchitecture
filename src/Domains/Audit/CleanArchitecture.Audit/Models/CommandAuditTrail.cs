namespace CleanArchitecture.Audit.Domain;

public class CommandAuditTrail : AuditTrail
{

#pragma warning disable

    private CommandAuditTrail() { }

#pragma warning restore

    public string Command { get; }

    public CommandAuditTrail(Guid correlationId, string actor, string domain, string command, string request, RequestParameters parameters)
        : base(correlationId, actor, domain, request, parameters)
    {
        Command = command;
    }
}
