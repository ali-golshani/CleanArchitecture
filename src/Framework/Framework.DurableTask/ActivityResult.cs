namespace Framework.DurableTask;

public readonly struct ActivityResult
{
    public static readonly ActivityResult Success = new(true, []);

    public static ActivityResult Failure(params string[] errors)
    {
        return new(false, errors);
    }

    public ActivityResult() { }

    internal ActivityResult(bool isSuccess, string[] errors)
    {
        IsSuccess = isSuccess;
        Errors = errors;
    }

    public bool IsSuccess { get; init; } = false;
    public string[] Errors { get; init; } = [];
}
