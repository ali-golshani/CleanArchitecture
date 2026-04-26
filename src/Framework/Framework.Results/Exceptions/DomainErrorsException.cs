using Framework.Exceptions;
using Framework.Exceptions.Extensions;

namespace Framework.Results.Exceptions;

public class DomainErrorsException : DomainException
{
    public DomainErrorsException(params Error[] errors)
        : this(false, errors)
    { }

    public DomainErrorsException(bool shouldLog, params Error[] errors)
        : base(errors.Select(x => x.Message).MultiLineJoin())
    {
        Errors = errors;
        Messages = [.. Errors.Select(x => x.Message)];
        ShouldLog = shouldLog;
    }

    public Error[] Errors { get; }
    public override IReadOnlyCollection<string> Messages { get; }
    public override bool ShouldLog { get; }
}
