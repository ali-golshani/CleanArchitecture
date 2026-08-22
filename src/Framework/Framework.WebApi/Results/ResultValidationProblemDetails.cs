using Microsoft.AspNetCore.Mvc;

namespace Framework.WebApi.Results;

public sealed class ResultValidationProblemDetails(Dictionary<string, string[]> errors) : ValidationProblemDetails(errors)
{
    public required string[] ErrorCodes { get; init; }
    public required string[] ErrorMessages { get; init; }
}
