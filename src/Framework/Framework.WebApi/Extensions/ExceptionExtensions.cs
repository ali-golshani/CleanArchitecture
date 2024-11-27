using Framework.Results;
using Microsoft.AspNetCore.Mvc;

namespace Framework.WebApi.Exceptions;

internal static class ExceptionExtensions
{
    public static ProblemDetails AsProblemDetails(this Exception exp)
    {
        var errors = ErrorMessages.DomainErrorMessages(exp);

        return new ResultProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = ErrorType.Failure.ToString(),
            Detail = errors[0],
            ErrorMessages = errors,
        };
    }
}
