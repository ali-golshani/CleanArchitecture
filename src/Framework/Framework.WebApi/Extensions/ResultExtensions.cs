using Framework.Results;
using Framework.WebApi.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Framework.WebApi.Extensions;

public static class ResultExtensions
{
    public static Results<Ok<T>, NotFound> AsOkOrNotFound<T>(this T? value)
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

    public static ProblemHttpResult AsProblemResult(this Error[] errors)
    {
        return ResultToProblemDetails.ToProblemResult(errors);
    }

    public static async Task<Results<NoContent, ProblemHttpResult>> AsTypedResults(this Task<Result<Empty>> resultTask)
    {
        var result = await resultTask;
        return ResultToProblemDetails.ToTypedResults(result);
    }

    public static Results<NoContent, ProblemHttpResult> AsTypedResults(this Result<Empty> result)
    {
        return ResultToProblemDetails.ToTypedResults(result);
    }

    public static async Task<Results<Ok<T>, ProblemHttpResult>> AsTypedResults<T>(this Task<Result<T>> resultTask)
    {
        var result = await resultTask;
        return ResultToProblemDetails.ToTypedResults(result);
    }

    public static Results<Ok<T>, ProblemHttpResult> AsTypedResults<T>(this Result<T> result)
    {
        return ResultToProblemDetails.ToTypedResults(result);
    }

    public static async Task<ActionResult> AsActionResult(this Task<Result<Empty>> resultTask)
    {
        var result = await resultTask;
        return ResultToProblemDetails.ToActionResult(result);
    }

    public static ActionResult AsActionResult(this Result<Empty> result)
    {
        return ResultToProblemDetails.ToActionResult(result);
    }

    public static async Task<ActionResult> AsActionResult<T>(this Task<Result<T>> resultTask)
    {
        var result = await resultTask;
        return ResultToProblemDetails.ToActionResult(result);
    }

    public static ActionResult AsActionResult<T>(this Result<T> result)
    {
        return ResultToProblemDetails.ToActionResult(result);
    }
}
