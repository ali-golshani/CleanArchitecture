using CleanArchitecture.Audit.Domain;

namespace CleanArchitecture.Audit;

public interface ICommandAuditAgent
{
    void Post(CommandAuditTrail logEntry);
}
