namespace CleanArchitecture.Mediator;

public class DomainErrorsException : DomainException
{
    private readonly bool shouldLog;

    public DomainErrorsException(params Error[] errors)
        : this(false, errors)
    { }

    public DomainErrorsException(bool shouldLog, params Error[] errors)
    {
        this.shouldLog = shouldLog;
        Errors = errors;
    }

    public Error[] Errors { get; }

    public override bool ShouldLog => shouldLog;
    public override string Message => MultiLines(Errors.Select(x => x.Message));

    public static implicit operator DomainErrorsException(Error error)
    {
        return new(error);
    }
}
