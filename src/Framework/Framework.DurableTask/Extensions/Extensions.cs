using Framework.Results;

namespace Framework.DurableTask.Extensions;

public static class Extensions
{
    public static ActivityResult<T> ToActivityResult<T>(this Result<T> result)
    {
        return new ActivityResult<T>(result.IsSuccess, result.Value, [.. result.Errors.Select(x => x.Message)]);
    }

    public static ActivityResult ToEmptyActivityResult<T>(this Result<T> result)
    {
        return new ActivityResult(result.IsSuccess, [.. result.Errors.Select(x => x.Message)]);
    }

    public static ActivityResult ToEmptyActivityResult(this Exception exp)
    {
        return new ActivityResult(exp.Message);
    }
}
