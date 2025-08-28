using Framework.Results;
using Framework.WebApi.Results;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Framework.WebApi.Extensions;

public static class ResultExtensions
{
    public static async Task<NoContent> ToNoContent(this Task task)
    {
        await task;
        return TypedResults.NoContent();
    }

    public static Results<Ok<T>, NotFound> ToOkOrNotFound<T>(this T? value)
    {
        if (value is null)
        {
            return TypedResults.NotFound();
        }
        else
        {
            return TypedResults.Ok(value);
        }
    }

    public static async Task<Results<Ok<T>, NotFound>> ToOkOrNotFound<T>(this Task<T?> valueTask)
    {
        return (await valueTask).ToOkOrNotFound();
    }

    public static Results<Ok<T>, NotFound, ProblemHttpResult> ToOkOrNotFoundOrProblem<T>(this Result<T?> result)
    {
        if (result.IsSuccess)
        {
            var value = result.Value;
            if (value is null)
            {
                return TypedResults.NotFound();
            }
            else
            {
                return TypedResults.Ok(value);
            }
        }
        else
        {
            return result.Errors.ToProblemResult();
        }
    }

    public static async Task<Results<Ok<T>, NotFound, ProblemHttpResult>> ToOkOrNotFoundOrProblem<T>(this Task<Result<T?>> resultTask)
    {
        var result = await resultTask;
        return result.ToOkOrNotFoundOrProblem();
    }

    public static async Task<Results<NoContent, ProblemHttpResult>> ToNoContentOrProblem(this Task<Result<Empty>> resultTask)
    {
        var result = await resultTask;
        return result.ToNoContentOrProblem();
    }

    public static Results<NoContent, ProblemHttpResult> ToNoContentOrProblem(this Result<Empty> result)
    {
        if (result.IsSuccess)
        {
            return TypedResults.NoContent();
        }
        else
        {
            return ToProblemResult(result.Errors);
        }
    }

    public static async Task<Results<Ok<T>, ProblemHttpResult>> ToOkOrProblem<T>(this Task<Result<T>> resultTask)
    {
        var result = await resultTask;
        return result.ToOkOrProblem();
    }

    public static Results<Ok<T>, ProblemHttpResult> ToOkOrProblem<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            return TypedResults.Ok(result.Value);
        }
        else
        {
            return ToProblemResult(result.Errors);
        }
    }

    public static ProblemHttpResult ToProblemResult(this Error[] errors)
    {
        return ErrorsToProblemResultConverter.ToProblemResult(errors);
    }
}
