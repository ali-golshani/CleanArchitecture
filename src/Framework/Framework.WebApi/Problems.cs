using Framework.Results.Errors;
using Framework.WebApi.Results;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Framework.WebApi;

public static class Problems
{
    public static ProblemHttpResult NotFoundProblem(NotFoundError notFound)
    {
        return NotFoundProblemResult(notFound);
    }

    private static ProblemHttpResult NotFoundProblemResult(NotFoundError notFound)
    {
        return ErrorsToProblemResultConverter.ToProblemResult([notFound]);
    }
}