namespace Framework.DurableTask;

public readonly struct ActivityResult
{
    public static readonly ActivityResult Success = new(true, []);

    public ActivityResult() { }

    public ActivityResult(params string[] errors)
        : this(false, errors)
    { }

    internal ActivityResult(bool isSuccess, string[] errors)
    {
        IsSuccess = isSuccess;
        Errors = errors;
    }

    public bool IsSuccess { get; init; }
    public string[] Errors { get; init; } = [];
}
