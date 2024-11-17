using Hellang.Middleware.ProblemDetails;
using Hellang.Middleware.ProblemDetails.Mvc;

namespace CleanArchitecture.WebApi.Shared.Configs;

public static class ProblemDetailsConfigs
{
    public static void Configure(this IServiceCollection services, bool includeExceptionDetails)
    {
        services.AddProblemDetails(options =>
        {
            options.IncludeExceptionDetails = (_, _) => includeExceptionDetails;
        }).AddProblemDetailsConventions();

    }
}