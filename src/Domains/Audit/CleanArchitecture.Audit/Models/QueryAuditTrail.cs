namespace CleanArchitecture.Audit.Domain;

public class QueryAuditTrail : AuditTrail
{

#pragma warning disable

    private QueryAuditTrail() { }

#pragma warning restore

    public string Query { get; }
    public bool? ShouldLog { get; }

    public QueryAuditTrail(Guid correlationId, string actor, string domain, string query, string request, RequestParameters parameters, bool? shouldLog)
        : base(correlationId, actor, domain, request, parameters)
    {
        Query = query;
        ShouldLog = shouldLog;
    }
}
