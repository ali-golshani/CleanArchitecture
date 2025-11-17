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
    public static Result<T> Failure(Error[] errors, string? correlationId = null) => new(false, default, errors, correlationId);

    public Result() { }

    private Result(bool isSuccess, T? value, Error[] errors, string? correlationId)
    {
        IsSuccess = isSuccess;
        Value = value;
        Errors = errors;
        CorrelationId = correlationId;
    }

    public bool IsSuccess { get; }
    public T? Value { get; }
    public Error[] Errors { get; }
    public string? CorrelationId { get; }

    public bool IsFailure => !IsSuccess;
}
