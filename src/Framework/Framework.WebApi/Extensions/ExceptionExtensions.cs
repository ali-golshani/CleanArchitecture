using Framework.Exceptions.Extensions;
using Framework.Results;
using Framework.WebApi.Results;
using Microsoft.AspNetCore.Mvc;

namespace Framework.WebApi.Extensions;

public static class ExceptionExtensions
{
    public static ProblemDetails ToInternalServerProblemDetails(this Exception exp)
    {
        var ex = exp.TranslateToSystemException();

        return new ResultProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = ErrorType.Failure.ToString(),
            Detail = ex.Message,
            ErrorMessages = [.. ex.Messages],
        };
    }
}
