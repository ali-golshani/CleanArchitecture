namespace Framework.Results.Extensions;

public static class ContinueOnSuccessExtensions
{
    public static async Task<Result<T2>> ContinueOnSuccess<T1, T2>(
        this Task<Result<T1>> result,
        Func<T1, Task<Result<T2>>> onSuccessFunction)
    {
        var value = await result;

        if (value.IsFailure)
        {
            return value.Errors;
        }

        return await onSuccessFunction(value.Value!);
    }

    public static async Task<Result<T>> ContinueOnSuccess<T>(
        this Task<Result<Empty>> result,
        Func<Task<Result<T>>> onSuccessFunction)
    {
        var value = await result;

        if (value.IsFailure)
        {
            return value.Errors;
        }

        return await onSuccessFunction();
    }

    public static async Task<Result<Empty>> ContinueOnSuccess(
        this Task<Result<Empty>> result,
        Func<Task<Result<Empty>>> onSuccessFunction)
    {
        var value = await result;

        if (value.IsFailure)
        {
            return value.Errors;
        }

        return await onSuccessFunction();
    }

    public static async Task<Result<Empty>> ContinueOnSuccess(
        this Task<Result<Empty>> result,
        Func<Task> onSuccessFunction)
    {
        var value = await result;

        if (value.IsFailure)
        {
            return value.Errors;
        }

        await onSuccessFunction();

        return Empty.Value;
    }

    public static async Task<Result<Empty>> ContinueOnSuccess(
        this Task<Result<Empty>> result,
        Func<ValueTask> onSuccessFunction)
    {
        var value = await result;

        if (value.IsFailure)
        {
            return value.Errors;
        }

        await onSuccessFunction();

        return Empty.Value;
    }
}
