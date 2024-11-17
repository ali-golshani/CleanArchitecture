namespace Framework.Exceptions;

public class ProgrammerException : BaseSystemException
{
    public ProgrammerException(string? technicalMessage = null)
    {
        TechnicalMessage = technicalMessage;
    }

    public string? TechnicalMessage { get; }

    public override bool ShouldLog => true;
    public override bool IsFatal => true;

    public override string Message => "خطای پیش بینی نشده";
}
