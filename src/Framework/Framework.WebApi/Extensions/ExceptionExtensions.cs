using Framework.Exceptions.Extensions;
using Framework.Exceptions;
using Framework.Results;
using Microsoft.AspNetCore.Mvc;

namespace Framework.WebApi.Exceptions;

public static class ExceptionExtensions
{
    public static ProblemDetails AsProblemDetails(this Exception exp)
    {
        var errors = ErrorMessages(exp);

        return new ResultProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = ErrorType.Failure.ToString(),
            Detail = errors[0],
            ErrorMessages = errors,
        };
    }

    private static string[] ErrorMessages(Exception exp)
    {
        var errors = exp.Errors().ToArray();

        if (errors.Length == 0)
        {
            errors = ["خطای نامشخص"];
        }

        return errors;
    }

    private static IReadOnlyCollection<string> Errors(this Exception exp)
    {
        exp = exp.UnwrapAll();
        return exp switch
        {
            UserFriendlyException friendlyException => friendlyException.Messages,
            BaseSystemException systemException => systemException.Messages,
            _ => [],
        };
    }
}
