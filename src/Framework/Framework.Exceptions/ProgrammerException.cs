namespace Framework.Exceptions;

public class ProgrammerException(string? technicalMessage = null)
    : BaseSystemException(Resources.ExceptionMessages.ProgrammerException)
{
    public string? TechnicalMessage { get; } = technicalMessage;

    public override bool IsFatal => true;
    public override bool ShouldLog => true;

    public override IEnumerable<Fact> Facts
    {
        get
        {
            yield return new(nameof(TechnicalMessage), TechnicalMessage);
        }
    }
}