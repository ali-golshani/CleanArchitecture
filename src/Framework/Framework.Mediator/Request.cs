namespace Framework.Mediator;

public abstract class Request
{
    public abstract string LggingDomain { get; }
    public abstract string RequestTitle { get; }
    public DateTime RequestTime { get; }
    public Guid CorrelationId { get; private set; }

    protected Request()
    {
        RequestTime = DateTime.Now;
        CorrelationId = Guid.NewGuid();
    }

    public virtual bool? ShouldLog => null;

    public void SetCorrelationId(Guid? correlationId)
    {
        if (correlationId != null)
        {
            CorrelationId = correlationId.Value;
        }
    }
}
