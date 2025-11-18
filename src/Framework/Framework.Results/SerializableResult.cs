namespace Framework.Results;

public sealed class SerializableResult<T>
{
    public static SerializableResult<T> FromResult(Result<T> result)
    {
        return new SerializableResult<T>
        {
            IsSuccess = result.IsSuccess,
            Value = result.Value,
            CorrelationId = result.CorrelationId,
            Errors = [.. result.Errors.Select(x => x.Message)],
        };
    }

    public SerializableResult() { }

    public SerializableResult(T value, string? correlationId = null)
        : this(true, value, [], correlationId)
    { }

    public SerializableResult(string[] errors, string? correlationId = null)
        : this(false, default, errors, correlationId)
    { }

    private SerializableResult(bool isSuccess, T? value, string[] errors, string? correlationId)
    {
        IsSuccess = isSuccess;
        Value = value;
        Errors = errors;
        CorrelationId = correlationId;
    }

    public bool IsSuccess { get; init; }
    public T? Value { get; init; }
    public string[] Errors { get; init; } = [];
    public string? CorrelationId { get; init; }
}
