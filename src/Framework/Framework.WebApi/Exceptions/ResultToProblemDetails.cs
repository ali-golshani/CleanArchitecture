using Framework.Results;
using Framework.Results.Errors;
using Framework.Results.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Framework.WebApi.Exceptions;

internal static class ResultToProblemDetails
{
    public static Results<NoContent, ProblemHttpResult> ToTypedResults(Result<Empty> result)
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

    public static Results<Ok<T>, ProblemHttpResult> ToTypedResults<T>(Result<T> result)
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

    public static ProblemHttpResult ToProblemResult(Error[] errors)
    {
        return TypedResults.Problem(ProblemDetails(errors));
    }

    private static ProblemDetails ProblemDetails(Error[] errors)
    {
        var error = errors.Length == 0 ? UnexpectedError.Default : errors[0];

        if (error.Type == ErrorType.Validation)
        {
            return new ResultValidationProblemDetails(ValidationErrors(errors))
            {
                ErrorMessages = Errors(errors),
            };
        }
        else
        {
            return new ResultProblemDetails
            {
                Status = StatusCode(error.Type),
                Title = ErrorTitle(error.Type),
                Detail = error.Message,
                ErrorMessages = Errors(errors),
            };
        }
    }

    private static int StatusCode(ErrorType type) => (int)type.AsHttpStatusCode();

    private static string ErrorTitle(ErrorType type)
    {
        return type.ToString();
    }

    private static string[] Errors(Error[] errors)
    {
        return errors.Select(x => x.Message).ToArray();
    }

    private static Dictionary<string, string[]> ValidationErrors(Error[] errors)
    {
        var dictionary = new Dictionary<string, List<string>>();

        foreach (var error in errors)
        {
            foreach (var item in error.Sources.Select(x => x.Name))
            {
                if (dictionary.TryGetValue(item, out var list))
                {
                    list.Add(error.Message);
                }
                else
                {
                    dictionary[item] = [error.Message];
                }
            }
        }

        var result = new Dictionary<string, string[]>();
        foreach (var item in dictionary)
        {
            result.Add(item.Key, [.. item.Value]);
        }
        return result;
    }
}
