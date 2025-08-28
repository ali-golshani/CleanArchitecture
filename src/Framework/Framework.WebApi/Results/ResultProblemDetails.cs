using Microsoft.AspNetCore.Mvc;

namespace Framework.WebApi.Results;

public sealed class ResultProblemDetails : ProblemDetails
{
    public required string[] ErrorMessages { get; init; }
}
