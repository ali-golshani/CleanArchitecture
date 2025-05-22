namespace Framework.DomainRules.Templates;

public sealed class StringRule(
    string? value,
    string source,
    bool acceptEmptyValue = false,
    int? minLength = 1,
    int? maxLength = null)
    : IDomainRule
{
    public string? Value { get; } = value;
    public int? MinLength { get; } = minLength;
    public int? MaxLength { get; } = maxLength;

    protected string Source { get; } = source;
    public bool AcceptEmptyValue { get; } = acceptEmptyValue;

    public IEnumerable<Clause> Evaluate()
    {
        yield return new Clause(
            isTrue: IsValid(),
            statement: Statement(),
            sources: new ClauseSource(Source, Value));
    }

    private string Statement()
    {
        if (MinLength == 1)
        {
            if (MaxLength == null)
            {
                return $"{Source} باید دارای مقدار باشد";
            }
            else
            {
                return $"{Source} باید دارای مقدار و حداکثر شامل {MaxLength.Value} حرف باشد";
            }
        }
        else if (MinLength > 1)
        {
            if (MaxLength == null)
            {
                return $"{Source} باید حداقل شامل {MinLength} حرف باشد";
            }
            else
            {
                return $"{Source} باید حداقل شامل {MinLength} حرف و حداکثر شامل {MaxLength} حرف باشد";
            }
        }
        else
        {
            if (MaxLength == null)
            {
                return string.Empty;
            }
            else
            {
                return $"{Source} باید حداکثر شامل {MaxLength} حرف باشد";
            }
        }
    }

    private bool IsValid()
    {
        var length = Value?.Trim().Length;

        return
            (AcceptEmptyValue || !string.IsNullOrWhiteSpace(Value)) &&
            (MinLength == null || length >= MinLength) &&
            (MaxLength == null || length <= MaxLength);
    }
}
