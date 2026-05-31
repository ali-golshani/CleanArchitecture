namespace Framework.Exceptions;

public class ProgrammerException(string? technicalMessage = null)
    : BaseSystemException(Resources.ExceptionMessages.ProgrammerException)
{
    public string? TechnicalMessage { get; } = technicalMessage;

    public override bool IsFatal => true;
    public override bool ShouldLog => true;

    public override IEnumerable<(string Name, object? Value)> LogProperties
    {
        get
        {
            yield return (nameof(TechnicalMessage), TechnicalMessage);
        }
    }
}