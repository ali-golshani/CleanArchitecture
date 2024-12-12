namespace Framework.Exceptions;

public class ProgrammerException(string? technicalMessage = null)
    : BaseSystemException(ExceptionMessages.ProgrammerException)
{
    public string? TechnicalMessage { get; } = technicalMessage;

    public override bool IsFatal => true;
    public override bool ShouldLog => true;
}