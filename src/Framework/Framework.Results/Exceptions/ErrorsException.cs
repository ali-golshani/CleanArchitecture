using Framework.Exceptions;

namespace Framework.Results.Exceptions;

public sealed class ErrorsException : DomainException
{
    public ErrorsException(params Error[] errors)
        : this(false, errors)
    { }

    public ErrorsException(bool shouldLog, params Error[] errors)
        : base(ErrorMessage(errors))
    {
        Errors = errors;
        ShouldLog = shouldLog;
        Messages = [.. Errors.Select(x => x.Message)];
    }

    public Error[] Errors { get; }
    public override bool ShouldLog { get; }
    public override IReadOnlyCollection<string> Messages { get; }
    public override IEnumerable<Fact> Facts => Errors.SelectMany(x => x.Facts);

    private static string ErrorMessage(Error[] errors)
    {
        return string.Join(Environment.NewLine, errors.Select(x => x.Message));
    }
}
