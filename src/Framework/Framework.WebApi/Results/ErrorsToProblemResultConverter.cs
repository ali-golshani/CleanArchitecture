using Framework.Results;
using Framework.Results.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Framework.WebApi.Results;

internal static class ErrorsToProblemResultConverter
{
    public static ProblemHttpResult ToProblemResult(Error[] errors)
    {
        return TypedResults.Problem(ToProblemDetails(errors));
    }

    private static ProblemDetails ToProblemDetails(Error[] errors)
    {
        var error = errors.Length == 0 ? Framework.Results.Errors.Unexpected : errors[0];

        if (error.Type == ErrorType.Validation)
        {
            return new ResultValidationProblemDetails(ValidationErrors(errors))
            {
                ErrorMessages = ErrorMessages(errors),
            };
        }
        else
        {
            return new ResultProblemDetails
            {
                Status = StatusCode(error.Type),
                Title = ErrorTitle(error.Type),
                Detail = error.Message,
                ErrorMessages = ErrorMessages(errors),
            };
        }
    }

    private static int StatusCode(ErrorType type) => (int)type.AsHttpStatusCode();

    private static string ErrorTitle(ErrorType type)
    {
        return type.ToString();
    }

    private static string[] ErrorMessages(Error[] errors)
    {
        return [.. errors.Select(x => x.Message)];
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
