using CleanArchitecture.UserManagement.Application.Services;
using CleanArchitecture.UserManagement.Application.Requests.UpdateUserClaims;
using Framework.WebApi;
using Framework.WebApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace CleanArchitecture.UserManagement.Endpoints.Users;

internal sealed class UpdateUserClaims : IMinimalEndpoint
{
    public static void AddRoute(IEndpointRouteBuilder app)
    {
        app
            .MapPatch("users/claims", Handle)
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
