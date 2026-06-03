using static Framework.DomainRules.Resources.RuleMessageBuilder;

namespace Framework.DomainRules.Templates;

public abstract class StringRule(
    string source,
    string? value,
    bool acceptEmptyValue = false,
    int? minLength = 1,
    int? maxLength = null)
    : IDomainRule
{
    public string Source { get; } = source;
    public string? Value { get; } = value;
    public int? MinLength { get; } = minLength;
    public int? MaxLength { get; } = maxLength;
    public bool AcceptEmptyValue { get; } = acceptEmptyValue;

    public IEnumerable<Error> Evaluate()
    {
        if (!IsValid())
        {
            yield return new Error(
                ErrorType.Validation,
                Statement(),
                (Source, Value));
        }
    }

    private string Statement()
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
                return string.Empty;
            }
            else
            {
                return StringRuleMessages.MaxLength(Source, MaxLength.Value);
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
