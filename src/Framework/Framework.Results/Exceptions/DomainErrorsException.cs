using Framework.Exceptions;
using Framework.Exceptions.Extensions;

namespace Framework.Results.Exceptions;

public class DomainErrorsException : DomainException
{
    public DomainErrorsException(params Error[] errors)
        : this(false, errors)
    { }

    public DomainErrorsException(bool shouldLog, params Error[] errors)
    {
        Errors = errors;
        Message = Errors.Select(x => x.Message).MultiLineJoin();
        Messages = Errors.Select(x => x.Message).ToList();
        ShouldLog = shouldLog;
    }

    public Error[] Errors { get; }
    public override string Message { get; }
    public override IReadOnlyCollection<string> Messages { get; }
    public override bool ShouldLog { get; }
}
