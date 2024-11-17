namespace Framework.Results.Extensions;

public static class Extensions
{
    public static T ThrowIsFailure<T>(this Result<T> result)
    {
        if (result.IsFailure)
        {
            throw new Exceptions.DomainErrorsException(result.Errors);
        }
        else
        {
            return result.Value!;
        }
    }

    public static async Task<T> ThrowIsFailure<T>(this Task<Result<T>> resultTask)
    {
        var result = await resultTask;
        return result.ThrowIsFailure();
    }

    public static Result<T> NotFoundIfNull<T>(this Result<T?> result, string resourceName, object? resourceKey = null)
        where T : class
    {
        if (result.IsFailure)
        {
            return result.Errors;
        }

        if (result.Value == null)
        {
            return new NotFoundError(resourceName, resourceKey);
        }

        return result.Value;
    }

    public static async Task<Result<T>> NotFoundIfNull<T>(this Task<Result<T?>> resultTask, string resourceName, object? resourceKey = null)
        where T : class
    {
        var result = await resultTask;
        return result.NotFoundIfNull(resourceName, resourceKey);
    }

    public static Result<Empty> AsEmptyResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
        {
            return Empty.Value;
        }
        else
        {
            return result.Errors;
        }
    }
}
