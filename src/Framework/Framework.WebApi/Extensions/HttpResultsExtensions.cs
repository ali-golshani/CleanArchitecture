using Framework.Results;
using Framework.WebApi.Results;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Framework.WebApi.Extensions;

public static class HttpResultsExtensions
{
    public static Results<Ok<T>, ProblemHttpResult> ToOkOrNotFound<T>(this T? value)
    {
        if (value is null)
        {
            return Problems.NotFoundProblem;
        }
        else
        {
            return TypedResults.Ok(value);
        }
    }

    public static Results<Ok<T>, ProblemHttpResult> ToOkOrNotFoundOrProblem<T>(this Result<T?> result)
    {
        if (result.IsSuccess)
        {
            var value = result.Value;
            if (value is null)
            {
                return Problems.NotFoundProblem;
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

    public static Results<Ok, ProblemHttpResult> ToOkOrProblem(this Result<Empty> result)
    {
        if (result.IsSuccess)
        {
            return TypedResults.Ok();
        }
        else
        {
            return ToProblemResult(result.Errors);
        }
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
