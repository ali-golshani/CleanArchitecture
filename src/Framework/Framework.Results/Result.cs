namespace Framework.Results;

public sealed class Result<T>
{
    public static implicit operator Result<T>(T value)
    {
        return Success(value);
    }

    public static implicit operator Result<T>(Error error)
    {
        return Failure([error]);
    }

    public static implicit operator Result<T>(Error[] errors)
    {
        return Failure(errors);
    }

    public static implicit operator Result<T>(List<Error> errors)
    {
        return Failure([.. errors]);
    }

    public static Result<T> Success(T value) => new(true, value, [], null);
    public static Result<T> Failure(Error[] errors, string? errorId = null) => new(false, default, errors, errorId);

    private Result(bool isSuccess, T? value, Error[] errors, string? errorId)
    {
        IsSuccess = isSuccess;
        Value = value;
        Errors = errors;
        ErrorId = errorId;
    }

    public bool IsSuccess { get; }
    public T? Value { get; }
    public Error[] Errors { get; }
    public string? ErrorId { get; }

    public bool IsFailure => !IsSuccess;

    public Result<TTarget> AsFailure<TTarget>()
    {
        if (IsSuccess)
        {
            throw new Framework.Exceptions.ProgrammerException("A successful result cannot be propagated as a failure.");
        }

        return Result<TTarget>.Failure(Errors, ErrorId);
    }
}
