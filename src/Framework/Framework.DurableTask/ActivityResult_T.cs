namespace Framework.DurableTask;

public readonly struct ActivityResult<T>
{
    public ActivityResult() { }

    public ActivityResult(T value)
        : this(true, value, [])
    { }

    public ActivityResult(string[] errors)
        : this(false, default, errors)
    { }

    internal ActivityResult(bool isSuccess, T? value, string[] errors)
    {
        IsSuccess = isSuccess;
        Value = value;
        Errors = errors;
    }

    public bool IsSuccess { get; init; }
    public T? Value { get; init; }
    public string[] Errors { get; init; } = [];
}
