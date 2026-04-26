using CleanArchitecture.UserManagement.Application.Services;
using Framework.WebApi;
using Framework.WebApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using CleanArchitecture.UserManagement.Application.Requests.Authentication.LoginByPassword;

namespace CleanArchitecture.UserManagement.Endpoints.Authentication;

internal sealed class LoginByPassword : IMinimalEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app
            .MapPost("login/password", Handle)
            .WithTags("Authentication")
            ;
    }

    private static
        Task<Results<Ok<Response>, ProblemHttpResult>>
        Handle(IRequestService requestService, [FromBody] Request request, CancellationToken cancellationToken)
    {
        return
            requestService
            .Handle(request, cancellationToken)
            .ToOkOrProblem();
    }
}
