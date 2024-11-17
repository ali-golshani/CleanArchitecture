using Microsoft.AspNetCore.Mvc;

namespace Framework.WebApi.Exceptions;

public sealed class ResultValidationProblemDetails : ValidationProblemDetails
{
    public ResultValidationProblemDetails(Dictionary<string, string[]> errors)
        : base(errors)
    { }

    public required string[] ErrorMessages { get; init; }
}