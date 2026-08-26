namespace Framework.Mediator;

public abstract class Request
{
    public abstract string LoggingDomain { get; }
    public abstract string RequestTitle { get; }
    public DateTime RequestTime { get; }

    protected Request()
    {
        RequestTime = DateTime.Now;
    }

    public virtual bool? ShouldLog => null;
}
