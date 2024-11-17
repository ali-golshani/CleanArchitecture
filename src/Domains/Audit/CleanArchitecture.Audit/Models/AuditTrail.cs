namespace CleanArchitecture.Audit.Domain;

public abstract class AuditTrail
{

#pragma warning disable

    private protected AuditTrail() { }

#pragma warning restore

    public long Id { get; }
    public Guid CorrelationId { get; }

    public string Actor { get; }
    public string Domain { get; }
    public string Request { get; }
    public string? Response { get; private set; }
    public bool IsSuccess { get; private set; }
    public RequestParameters Parameters { get; private set; }

    public DateTime RequestTime { get; }
    public TimeSpan ResponseTime { get; private set; }

    public void Succeed(TimeSpan responseTime, string? response = null, RequestParameters? parameters = null)
    {
        ResponseTime = responseTime;
        Response = response ?? parameters?.ToString();
        Parameters = Parameters.Update(parameters);
        IsSuccess = true;
    }

    public void Failed(Error[] errors, TimeSpan responseTime)
    {
        Response = errors.FailureMessage();
        ResponseTime = responseTime;
        IsSuccess = false;
    }

    public void Failed(Exception exp, TimeSpan responseTime)
    {
        Response = exp.FailureMessage();
        ResponseTime = responseTime;
        IsSuccess = false;
    }

    private protected AuditTrail(Guid correlationId, string actor, string domain, string request, RequestParameters parameters)
    {
        CorrelationId = correlationId;
        Actor = actor;
        Domain = domain;
        Request = request;
        Parameters = parameters;
        Response = null;
        IsSuccess = false;
        RequestTime = SystemDateTime.Now;
        ResponseTime = TimeSpan.Zero;
    }
}
