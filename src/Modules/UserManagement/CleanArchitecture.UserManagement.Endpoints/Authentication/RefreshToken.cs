using CleanArchitecture.UserManagement.Application.Services;
using Framework.WebApi;
using Framework.WebApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using CleanArchitecture.UserManagement.Application.Requests.Authentication.RefreshToken;

namespace CleanArchitecture.UserManagement.Endpoints.Authentication;

internal sealed class RefreshToken : IMinimalEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app
            .MapPost("tokens/refresh", Handle)
            .WithTags("Authentication")
            .RequireAuthorization()
            ;
    }

    private static
        async Task<Results<Ok<Response>, ProblemHttpResult>>
        Handle(IRequestService requestService, [FromBody] Request request, CancellationToken cancellationToken)
    {
        var result = await requestService.Handle(request, cancellationToken);
        return result.ToOkOrProblem();
    }
}
