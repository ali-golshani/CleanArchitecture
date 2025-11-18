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

    public bool IsSuccess { get; init; }
    public T? Value { get; init; }
    public string[] Errors { get; init; } = [];
    public string? CorrelationId { get; init; }
}
