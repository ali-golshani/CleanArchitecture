using System.ComponentModel;

namespace CleanArchitecture.Mediator;

public abstract class Request
{
    [ReadOnly(true)]
    public abstract string RequestTitle { get; }

    [ReadOnly(true)]
    public DateTime RequestTime { get; }

    [ReadOnly(true)]
    public Guid CorrelationId { get; private set; }

    protected Request()
    {
        RequestTime = DateTime.Now;
        CorrelationId = Guid.NewGuid();
    }

    [ReadOnly(true)]
    public virtual bool? ShouldLog => null;

    internal void SetCorrelationId(Guid? correlationId)
    {
        CorrelationId = correlationId ?? CorrelationId;
    }
}
