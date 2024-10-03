namespace CleanArchitecture.Mediator;

public abstract class Request
{
    public abstract string RequestTitle { get; }

    public DateTime RequestTime { get; }
    public Guid CorrelationId { get; }

    private protected Request()
    {
        RequestTime = DateTime.Now;
        CorrelationId = Guid.NewGuid();
    }

    public virtual bool? ShouldLog => null;
}
