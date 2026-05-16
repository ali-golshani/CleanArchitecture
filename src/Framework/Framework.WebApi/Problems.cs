using Framework.Results.Errors;
using Framework.WebApi.Results;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Framework.WebApi;

public static class Problems
{
    public static readonly ProblemHttpResult NotFoundProblem = NotFoundProblemResult();

    private static ProblemHttpResult NotFoundProblemResult()
    {
        return ErrorsToProblemResultConverter.ToProblemResult([NotFoundError.Default]);
    }

}