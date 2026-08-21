namespace Framework.Exceptions;

public class ProgrammerException(string? technicalMessage = null)
    : BaseSystemException(Resources.ExceptionMessages.ProgrammerException)
{
    public string? TechnicalMessage { get; } = technicalMessage;

    public override IEnumerable<Fact> Facts
    {
        get
        {
            yield return new(nameof(TechnicalMessage), TechnicalMessage);
        }
    }
}