using static Framework.DomainRules.Resources.RuleMessageBuilder;

namespace Framework.DomainRules.Templates;

public abstract class StringRule : IDomainRule
{
    protected StringRule(
        string code,
        string source,
        string? value,
        bool acceptEmptyValue = false,
        int? minLength = 1,
        int? maxLength = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(code);
        ArgumentException.ThrowIfNullOrWhiteSpace(source);

        if (minLength < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(minLength));
        }

        if (maxLength < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maxLength));
        }

        if (minLength > maxLength)
        {
            throw new ArgumentException("Minimum length cannot be greater than maximum length.", nameof(minLength));
        }

        Code = code;
        Source = source;
        Value = value;
        AcceptEmptyValue = acceptEmptyValue;
        MinLength = minLength;
        MaxLength = maxLength;
    }

    public string Code { get; }
    public string Source { get; }
    public string? Value { get; }
    public int? MinLength { get; }
    public int? MaxLength { get; }
    public bool AcceptEmptyValue { get; }

    public IEnumerable<Error> Evaluate()
    {
        if (!IsValid())
        {
            yield return new Error(
                Code,
                ErrorType.Validation,
                Statement()!,
                (Source, Value));
        }
    }

    private string? Statement()
    {
        if (MinLength == 1)
        {
            if (MaxLength == null)
            {
                return StringRuleMessages.NotEmpty(Source);
            }
            else
            {
                return StringRuleMessages.NotEmptyMaxLength(Source, MaxLength.Value);
            }
        }
        else if (MinLength > 1)
        {
            if (MaxLength == null)
            {
                return StringRuleMessages.MinLength(Source, MinLength.Value);
            }
            else
            {
                return StringRuleMessages.MinMaxLength(Source, MinLength.Value, MaxLength.Value);
            }
        }
        else
        {
            if (MaxLength == null)
            {
                return null;
            }
            else
            {
                return StringRuleMessages.MaxLength(Source, MaxLength.Value);
            }
        }
    }

    private bool IsValid()
    {
        var length = Value?.Trim().Length ?? 0;

        return
            (AcceptEmptyValue || !string.IsNullOrWhiteSpace(Value)) &&
            (MinLength == null || length >= MinLength) &&
            (MaxLength == null || length <= MaxLength);
    }
}
