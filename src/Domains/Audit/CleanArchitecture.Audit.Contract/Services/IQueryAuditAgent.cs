using CleanArchitecture.Audit.Domain;

namespace CleanArchitecture.Audit;

public interface IQueryAuditAgent
{
    void Post(QueryAuditTrail logEntry);
}
