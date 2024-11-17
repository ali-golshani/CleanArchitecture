namespace Framework.Domain;

public abstract class CommandAwareEntity
{
    public Guid? InsertCommandCorrelationId { get; set; } = null;
    public Guid? UpdateCommandCorrelationId { get; set; } = null;
}
