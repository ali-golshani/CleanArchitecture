using CleanArchitecture.UserManagement.Application.Services;
using Framework.WebApi;
using Framework.WebApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using CleanArchitecture.UserManagement.Application.Requests.Users.ChangePassword;

namespace CleanArchitecture.UserManagement.Endpoints.Users;

internal sealed class ChangePassword : IMinimalEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app
            .MapPost("users/change-password", Handle)
            .WithTags("Users")
            .RequireAuthorization()
            ;
    }

    private static
        Task<Results<Ok, ProblemHttpResult>>
        Handle(IRequestService requestService, [FromBody] Request request, CancellationToken cancellationToken)
    {
        return
            requestService
            .Handle(request, cancellationToken)
            .ToOkOrProblem();
    }
}
