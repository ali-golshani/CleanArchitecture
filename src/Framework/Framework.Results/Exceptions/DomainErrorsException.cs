using Framework.Exceptions;

namespace Framework.Results.Exceptions;

public sealed class DomainErrorsException : DomainException
{
    public DomainErrorsException(params Error[] errors)
        : this(false, errors)
    { }

    public DomainErrorsException(bool shouldLog, params Error[] errors)
        : base(ErrorMessage(errors))
    {
        Errors = errors;
        Messages = [.. Errors.Select(x => x.Message)];
        ShouldLog = shouldLog;
    }

    public Error[] Errors { get; }
    public override IReadOnlyCollection<string> Messages { get; }
    public override bool ShouldLog { get; }

    public override IEnumerable<(string Name, object? Value)> LogProperties
    {
        get
        {
            foreach (var error in Errors)
            {
                yield return (error.Message, string.Join(" ; ", error.Sources));
            }
        }
    }

    private static string ErrorMessage(Error[] errors)
    {
        return string.Join(Environment.NewLine, errors.Select(x => x.Message));
    }
}
