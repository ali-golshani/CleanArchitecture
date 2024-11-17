using Microsoft.AspNetCore.Mvc;

namespace Framework.WebApi.Exceptions;

public sealed class ResultProblemDetails : ProblemDetails
{
    public required string[] ErrorMessages { get; init; }
}
