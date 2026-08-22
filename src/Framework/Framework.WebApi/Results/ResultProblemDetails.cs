using Microsoft.AspNetCore.Mvc;

namespace Framework.WebApi.Results;

public sealed class ResultProblemDetails : ProblemDetails
{
    public string? ErrorId { get; init; }
    public required string[] ErrorCodes { get; init; }
    public required string[] ErrorMessages { get; init; }
}
